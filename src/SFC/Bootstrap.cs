using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using SFC.Infrastructure.Features.TimeDependency;
using SFC.Infrastructure.Features.Tracing;
using SFC.Infrastructure.Features.Validation;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Database;
using SFC.Infrastructure.Interfaces.Modules;
using SFC.Infrastructure.Interfaces.TimeDependency;
using SFC.Infrastructure.Interfaces.Tracing;

namespace SFC
{
  public class Bootstrap
  {
    static IEnumerable<Infrastructure.Interfaces.Modules.IModule> _modules;

    public static WebApplication Run(string[] args, string url, IEnumerable<Infrastructure.Interfaces.Modules.IModule> modules, Action<ContainerBuilder> overrideDependencies = null)
    {
      _modules = modules;

      Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .CreateBootstrapLogger();

      Log.Information("Starting up");

      var builder = WebApplication.CreateBuilder(args);

      string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

      builder.WebHost.UseUrls(url);

      builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Debug()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

      builder.Services.AddHangfire(conf => conf
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString)
        );

      

      builder.Services.AddHangfireServer();      
      builder.Services.AddControllers();

      var mvc = builder.Services.AddMvc()        
        .AddTracing();

      foreach (var m in _modules)
      {
        mvc.AddApplicationPart(m.GetType().Assembly);
      }

      foreach (var m in _modules.Where(f => f is IHaveAspConfiguration).Select(f => (IHaveAspConfiguration)f))
      {
        m.Configure(builder);        
      }
      mvc.AddControllersAsServices();

      builder.Services.AddHttpContextAccessor();
      builder.Services.AddFluentValidationAutoValidation();

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c =>
       {
         c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmogFightClub API", Version = "v1" });

         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
           Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
           Name = "Authorization",
           In = ParameterLocation.Header,
           Type = SecuritySchemeType.ApiKey,
           Scheme = "Bearer"
         });

         c.AddSecurityRequirement(new OpenApiSecurityRequirement()
         { {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
                {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

              },
              new List<string>()
           } });
       });

      builder.Services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(o =>
      {
        var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["JWT:Issuer"],
          ValidAudience = builder.Configuration["JWT:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Key)
        };
      });

      builder.Services.AddApiVersioning(o =>
      {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.ReportApiVersions = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
      });

      builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
      
      // Register services directly with Autofac here.
      // Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
      builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
      {
        builder.RegisterInstance(new ConnectionString(connectionString));
        foreach (var m in _modules.Where(f => f is IHaveAutofacRegistrations).Select(f => (IHaveAutofacRegistrations)f))
        {
          m.RegisterTypes(builder);
        }
        overrideDependencies?.Invoke(builder);
      });

      var app = builder.Build();

      GlobalConfiguration.Configuration.UseActivator(app.Services.GetService<JobActivator>());
      GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

      app.Services.GetService<IDatabaseMigrator>().Run();
      app.Services.GetService<IScheduler>().RegisterRecurrentTasks();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
          
      app.UseSerilogRequestLogging();

      app.UseAuthentication();      

      app.UseAuthorization();

      app.UseHangfireDashboard();

      app.MapControllers();      

      app.Start();

      foreach (var m in _modules.Where(f => f is IHaveAspConfiguration).Select(f => (IHaveAspConfiguration)f))
      {
        m.Configure(app);        
      }

      foreach (var module in _modules.Where(f=> f is IHaveWorker).Select(f=>(IHaveWorker)f)) 
      {
        module.StartWorker(app.Services.GetService<IComponentContext>());
      }

      return app;
    }

    public static void Stop(WebApplication app)
    {
      _modules.Where(f => f is IHaveWorker).ToList().ForEach(
       f => ((IHaveWorker)f).StopWorker());

      _modules.Where(f => f is IHaveWorker).ToList().ForEach(
       f => ((IHaveWorker)f).WaitForShutdown());

      app.StopAsync().Wait();
      app.WaitForShutdown();      
    }
  }
}
