using Autofac;
using SFC.Infrastructure.Fake;
using SFC.Infrastructure.Interfaces;

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
      builder.RegisterGenericDecorator(typeof(ValidationCommandHandler<>), typeof(ICommandHandler<>));
      builder.RegisterGenericDecorator(typeof(ValidationQueryHandler<,>), typeof(IQueryHandler<,>));
    }
  }
}
