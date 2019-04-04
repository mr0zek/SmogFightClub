using Autofac;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.Notifications.Infrastructure;
using SFC.Processes;
using SFC.Sensors;

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
      builder.RegisterModule(new AutofacAdminApiModule(_connectionString));
      builder.RegisterModule(new AutofacAlertsModule(_connectionString));
      builder.RegisterModule(new AutofacProcessesModule(_connectionString));
      builder.RegisterModule(new AutofacNotificationsModule(_connectionString));
      builder.RegisterModule(new AutofacSensorsModule(_connectionString));
      builder.RegisterModule(new AutofacAccountsModule(_connectionString));
      builder.RegisterModule(new AutofacInfrastructureModule());
    }
  }
}