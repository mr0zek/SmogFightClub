using Autofac;
using FluentValidation;
using SFC.AuthenticationApi.Features.Authentication;

namespace SFC.AuthenticationApi
{
    public class AutofacAuthenticationApiModule : Module
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
