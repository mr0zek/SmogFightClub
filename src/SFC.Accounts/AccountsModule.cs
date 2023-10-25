using Autofac;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SFC.Accounts.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.Accounts
{
  [ModuleDefinition("Backend")]
  public class AccountsModule : Module, IModule
  {
    public void ConfigureMvc(IMvcBuilder builder)
    {
    }

    public void ConfigureWebApplication(WebApplication app)
    {
    }
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<AccountsRepository>()
        .AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(GetType().Assembly)
           .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
           .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
