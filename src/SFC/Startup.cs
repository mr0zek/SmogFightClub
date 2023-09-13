using System;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SFC.AdminApi;
using SFC.Infrastructure;
using SFC.SensorApi.Features.RecordMeasurement;
using SFC.UserApi;
using Swashbuckle.AspNetCore.Swagger;

namespace SFC
{
  public class Startup
  {
    public static Action<ContainerBuilder> RegisterExternalTypes { get; set; } = builder => { };

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      // Add framework services.
      services
        .AddMvc(opt => opt.Filters.Add(typeof(FluentValidationActionFilter)))
        .AddApplicationPart(typeof(AutofacUserApiModule).Assembly)
        .AddApplicationPart(typeof(AutofacAdminApiModule).Assembly)
        .AddApplicationPart(typeof(MeasurementsController).Assembly)
        .AddControllersAsServices()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

      services.AddLogging(loggingBuilder =>
      {
        loggingBuilder
          .AddConsole()
          .AddConfiguration(Configuration.GetSection("logging"))
          .AddDebug();
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmogFightClub API", Version = "v1" });
      });

      services.AddApiVersioning(o =>
      {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.ReportApiVersions = true;
        o.DefaultApiVersion = new ApiVersion(1, 0);
      });

      var builder = new ContainerBuilder();
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      builder.RegisterModule(new MainModule(connectionString));
      RegisterExternalTypes(builder);
      builder.Populate(services);
      var container = builder.Build();
      return new AutofacServiceProvider(container);
    }    
  }
}
