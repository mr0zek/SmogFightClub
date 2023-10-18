using Autofac;
using FluentValidation;
using SFC.AuthenticationApi.Features.Authentication;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.AuthenticationApi
{
  [ModuleDefinition("Api")]
  public class AuthenticationApiModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<TokenRepository>().AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
