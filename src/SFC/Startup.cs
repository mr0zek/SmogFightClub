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
using SFC.Infrastructure;
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
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
        .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Masterpiece API", Version = "v1" });
      });

      var builder = new ContainerBuilder();
      string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      builder.RegisterModule(new MainModule(connectionString));
      RegisterExternalTypes(builder);
      builder.Populate(services);
      var container = builder.Build();
      return new AutofacServiceProvider(container);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
