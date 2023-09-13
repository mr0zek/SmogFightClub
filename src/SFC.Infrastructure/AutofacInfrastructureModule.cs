using Autofac;

namespace SFC.Infrastructure
{
  public class AutofacInfrastructureModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {   
      builder.RegisterType<Bus>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
    }
  }
}
