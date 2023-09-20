using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using SFC.AdminApi;
using SFC.Infrastructure;
using SFC.SensorApi;
using SFC.SensorApi.Features.RecordMeasurement;
using SFC.UserApi;

namespace SFC
{
  public class Bootstrap
  {
    public static void Run(string[] args, Action<ContainerBuilder> overrideDependencies = null)
    {
      Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .CreateBootstrapLogger();

      Log.Information("Starting up");
      
      var builder = WebApplication.CreateBuilder(args);

      builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

      
      builder.Services.AddControllers();
      builder.Services
        .AddMvc(opt => opt.Filters.Add(typeof(FluentValidationActionFilter)))
          .AddApplicationPart(typeof(MainModule).Assembly)
          .AddApplicationPart(typeof(AutofacUserApiModule).Assembly)
          .AddApplicationPart(typeof(AutofacAdminApiModule).Assembly)
          .AddApplicationPart(typeof(AutofacSensorApiModule).Assembly)
          .AddApplicationPart(typeof(MeasurementsController).Assembly)
          .AddControllersAsServices();

      builder.Services.AddValidatorsFromAssembly(typeof(Bootstrap).Assembly);      

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c =>
       {
         c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmogFightClub API", Version = "v1" });
       });

      builder.Services.AddApiVersioning(o =>
      {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.ReportApiVersions = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
      });

      builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

      BaseUrl.Current = "http://localhost:5000/";

      string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

      // Register services directly with Autofac here.
      // Don't call builder.Populate(), that happens in AutofacServiceProviderFactory.
      builder.Host.ConfigureContainer<ContainerBuilder>(builder => {        
        builder.RegisterModule(new MainModule(connectionString));
        if (overrideDependencies != null)
        {
          overrideDependencies(builder);
        }
      });

      var app = builder.Build();
      
      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }      

      app.UseSerilogRequestLogging();

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();      

      ThreadPool.QueueUserWorkItem(state => { app.Run(); });
    }
  }
}
