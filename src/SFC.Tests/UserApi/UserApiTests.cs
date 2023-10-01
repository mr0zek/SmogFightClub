using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Fake;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes;
using SFC.Sensors;
using SFC.SharedKernel;
using SFC.Tests.DbMigrations;
using SFC.Tests.Mocks;
using SFC.UserApi;
using SFC.UserApi.Features.Accounts;
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

      SFC.Infrastructure.DbMigrations.Run(connectionString);
      InitializeDb.Init(connectionString);

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(new string[0], new Module[]
        {
          new AutofacUserApiModule(),
          new AutofacAccountsModule(),
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
      await RestClient.For<IUserApi>(_url).PostUser(new PostUserModel("noreply@example.com"));

      // Act
      await RestClient.For<IUserApi>(_url).PostAlert(
        new PostAlertModel()
        {
          ZipCode = "01-102"
        });

      // Assert
      Assert.Equal(1, TestSmtpClient.SentEmails.Count);

    }

    [Fact]
    public async void AccountCreationSuccessScenario()
    {
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };
      var provider = (FakeIdentityProvider)_app.Services.GetService(typeof(FakeIdentityProvider));
      provider.SetLoginName(postAccountModel.LoginName);

      string confirmationId = await RestClient.For<IUserApi>(_url).PostAccount(postAccountModel);

      Assert.Single(TestSmtpClient.SentEmails);

      await RestClient.For<IUserApi>(_url).PostAccountConfirmation(confirmationId);

      Assert.Equal(2, TestSmtpClient.SentEmails.Count);

      var alerts = await RestClient.For<IUserApi>(_url).GetAlerts();

      Assert.Single(alerts.Alerts);
      Assert.Equal(postAccountModel.ZipCode, alerts.Alerts.First().ZipCode);

    }
  }
} 
