using Autofac;

namespace SFC.Infrastructure.Fake
{
  public class AutofacFakeInfrastructureModule : Module
  {    
    protected override void Load(ContainerBuilder builder)
    {   
      builder.RegisterType<FakeIdentityProvider>().AsImplementedInterfaces();
      builder.RegisterType<FakeSmtpClient>().AsImplementedInterfaces();
    }
  }
}
