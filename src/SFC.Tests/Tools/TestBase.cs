using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.GiosGateway;
using SFC.Infrastructure;
using SFC.Infrastructure.Features.Database;
using SFC.Infrastructure.Interfaces.Modules;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Tests.AdminApi;
using SFC.Tests.Tools.DBReset;
using SFC.Tests.Tools.Mocks;
using SFC.Tests.UserApi;
using SFC.UserApi;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;

namespace SFC.Tests.Tools
{
  public abstract class TestBase : IDisposable
  {
    protected readonly string _url = TestHelper.GenerateUrl();
    private readonly WebApplication _app;


    protected TestBase()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"].ThrowIfNull();

      ResetDatabase.Reset(connectionString);

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(Array.Empty<string>(), _url, new IModule[]
        {
          new AuthenticationApiModule(),
          new AdminApiModule(),
          new UserApiModule(),
          new SensorApiModule(),
          new AccountsModule(),
          new SensorsModule(),
          new AlertsModule(),
          new ProcessesModule(),
          new NotificationsModule(),
          new InfrastructureModule(),
          new GiosGatewayModule()
        },
        builder =>
        {
          builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
        });
    }

    public void Dispose()
    {
      Bootstrap.Stop(_app);
    }
  }

}