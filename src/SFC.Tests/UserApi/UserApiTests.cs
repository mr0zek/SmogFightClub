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
    private readonly string _url = TestHelper.GenerateUrl();
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

    [Fact]
    public async void NotificationShoudBeSentAfterAlertCreation()
    {
      // Arrange
      var userApi = RestClient.For<IUserApi>(_url);
      var authApi = RestClient.For<IAuthenticationApi>(_url);      
      
      userApi.Token = "Bearer " + await authApi.Login(new CredentialsModel("admin", "password"));

      await userApi.PostUser(new PostUserModel("noreply@example.com"));

      // Act
      await userApi.PostAlert(
        new PostAlertModel()
        {
          ZipCode = "01-102"
        });

      // Assert
      Assert.Single(TestSmtpClient.SentEmails);

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
      string confirmationId = await RestClient.For<IUserApi>(_url).PostAccount(postAccountModel);
      await RestClient.For<IUserApi>(_url).PostAccountConfirmation(confirmationId);

      // Assert
      Assert.Equal(2, TestSmtpClient.SentEmails.Count);

      string token = await RestClient.For<IAuthenticationApi>(_url).Login(new (postAccountModel.LoginName, postAccountModel.Password));

      var api = RestClient.For<IUserApi>(_url);
      api.Token = $"Bearer {token}";

      var alerts = await api.GetAlerts();

      Assert.Single(alerts.Alerts);
      Assert.Equal(postAccountModel.ZipCode, alerts.Alerts.First().ZipCode);

    }
  }
} 
