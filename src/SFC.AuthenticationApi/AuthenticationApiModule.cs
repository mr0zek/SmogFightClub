using Autofac;
using FluentValidation;
using SFC.AuthenticationApi.Features.Authentication;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Infrastructure.Interfaces.Modules;

namespace SFC.AuthenticationApi
{
  [ModuleDefinition("Api")]
  public class AuthenticationApiModule : IHaveAutofacRegistrations, IModule 
  {
    public void RegisterTypes(ContainerBuilder builder)
    {
      builder.RegisterType<TokenRepository>().AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
