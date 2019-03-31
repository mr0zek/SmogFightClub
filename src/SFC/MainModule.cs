using Autofac;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.Processes;
using SFC.Sensors;
using SFC.Users;

namespace SFC
{
  public class MainModule : Module
  {
    private readonly string _connectionString;

    public MainModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterModule(new AutofacAlertsModule(_connectionString));
      builder.RegisterModule(new AutofacProcessesModule(_connectionString));
      builder.RegisterModule(new AutofacNotificationsModule(_connectionString));
      builder.RegisterModule(new AutofacSensorsModule(_connectionString));
      builder.RegisterModule(new AutofacUsersModule(_connectionString));
      builder.RegisterModule(new AutofacInfrastructureModule());
    }
  }
}