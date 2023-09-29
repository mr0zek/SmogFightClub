using Autofac;
using SFC.Infrastructure.Fake;

namespace SFC.Infrastructure
{
  public class AutofacInfrastructureModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {   
      builder.RegisterType<Bus>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterInstance(new FakeIdentityProvider()).AsSelf().AsImplementedInterfaces();
      builder.RegisterType<FakeSmtpClient>().AsImplementedInterfaces();
    }
  }
}
