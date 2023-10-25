using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SFC.AuthenticationApi.Features.Authentication;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.AuthenticationApi
{
  [ModuleDefinition("Api")]
  public class AuthenticationApiModule : Module, IModule
  {
    public void ConfigureMvc(IMvcBuilder builder)
    {
    }

    public void ConfigureWebApplication(WebApplication app)
    {
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<TokenRepository>().AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
