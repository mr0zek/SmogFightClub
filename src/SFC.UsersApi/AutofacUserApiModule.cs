using Autofac;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.UserApi
{
  public class AutofacUserApiModule : Module
  {   
    protected override void Load(ContainerBuilder builder)
    {       
      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(ICommandHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();

      builder.RegisterAssemblyTypes(GetType().Assembly)
        .AsClosedTypesOf(typeof(IEventHandler<>)).AsImplementedInterfaces()
        .InstancePerLifetimeScope();
    }
  }
}
