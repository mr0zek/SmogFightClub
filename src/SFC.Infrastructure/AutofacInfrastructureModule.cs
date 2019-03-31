using Autofac;

namespace SFC.Infrastructure
{
  public class AutofacInfrastructureModule : Module
  {
    public static void RegisterSelf(ContainerBuilder builder)
    {
      IContainer container = null;
      builder.Register(c => container).AsSelf();
      builder.RegisterBuildCallback(c => container = c);
    }

    protected override void Load(ContainerBuilder builder)
    {
      RegisterSelf(builder);
      builder.RegisterType<Bus>().AsImplementedInterfaces();
    }
  }
}
