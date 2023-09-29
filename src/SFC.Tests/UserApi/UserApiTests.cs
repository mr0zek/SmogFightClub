using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes;
using SFC.Sensors;
using SFC.SharedKernel;
using SFC.Tests.Mocks;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.UserApi
{
  public class UserApiTests
  {
    private const string _url = "http://localhost:5000";
    private readonly WebApplication _app;

    public UserApiTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);
      InitializeDb.InitializeDb.Init(connectionString);

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(new string[0], new Module[]
        {
          new AutofacUserApiModule(),
          new AutofacSensorsModule(),
          new AutofacAlertsModule(),
          new AutofacProcessesModule(),
          new AutofacNotificationsModule(),
          new AutofacInfrastructureModule()
        },
        builder =>
        {
          builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
        });      
    }

    [Fact]
    public async void NotificationShoudBeSentAfterAlertCreation()
    {
      // Arrange
      var commandBus = (ICommandBus)_app.Services.GetService(typeof(ICommandBus));
      IIdentityProvider identityProvider = (IIdentityProvider)_app.Services.GetService(typeof(IIdentityProvider));
      commandBus.Send(new SetNotificationEmailCommand("text@example.com", identityProvider.GetLoginName()));

      // Act
      await RestClient.For<IUserApi>(_url).PostAlert(
        new PostAlertModel() 
        { 
          ZipCode = "01-102" 
        });

      // Assert
      Assert.Single(TestSmtpClient.SentEmails);
    }
  }
}
