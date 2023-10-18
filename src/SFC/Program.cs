using System;
using Autofac;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.GiosGateway;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.UserApi;

namespace SFC
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);

      Bootstrap.Run(args,"http://localhost:5000", new Module[]
      {
        new AuthenticationApiModule(),
        new AdminApiModule(),
        new SensorApiModule(),
        new UserApiModule(),
        new AlertsModule(),
        new ProcessesModule(),
        new NotificationsModule(),
        new SensorsModule(),
        new AccountsModule(),
        new InfrastructureModule(),
        new GiosGatewayModule()
      });

      Console.ReadKey();
    }
  }
}
