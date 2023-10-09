using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Infrastructure.Fake;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes;
using SFC.Sensors;
using SFC.SharedKernel;
using SFC.Tests.Api;
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

    public class UserApiTests : IDisposable
  {
    private readonly string _url = TestHelper.GenerateUrl();
    private readonly WebApplication _app;

    public UserApiTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      SFC.Infrastructure.DbMigrations.Run(connectionString);

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(Array.Empty<string>(), _url, new Module[]
        {
          new AutofacAuthenticationApiModule(),
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

    public void Dispose()
    {
      Bootstrap.Stop(_app);
    }

    [Fact]
    public async void NotificationShoudBeSentAfterAlertCreation()
    {
      // Arrange
      var api = RestClient.For<IApi>(_url);

      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };
      
      string confirmationId = await RestClient.For<IApi>(_url).PostAccount(postAccountModel);
      await RestClient.For<IApi>(_url).PostAccountConfirmation(confirmationId);

      api.Token = "Bearer " + await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      await api.PostUser(new PostUserModel("noreply@example.com"));

      // Act
      await api.PostAlert(
        new PostAlertModel()
        {
          ZipCode = "01-102"
        });

      // Assert
      Assert.Equal(3, TestSmtpClient.SentEmails.Count);

    }

    [Fact]
    public async void AccountCreationSuccessScenario()
    {
      // Arrange
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      // Act
      var api = RestClient.For<IApi>(_url);
      string confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      // Assert
      Assert.Equal(2, TestSmtpClient.SentEmails.Count);

      api.Token = $"Bearer " + await RestClient.For<IApi>(_url).Login(new (postAccountModel.LoginName, postAccountModel.Password));      

      var alerts = await api.GetAlerts();

      Assert.Single(alerts.Alerts);
      Assert.Equal(postAccountModel.ZipCode, alerts.Alerts.First().ZipCode);

    }
  }
} 
