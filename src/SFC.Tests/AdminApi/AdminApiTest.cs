using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Infrastructure.Fake;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.SharedKernel;
using SFC.Tests.DbMigrations;
using SFC.Tests.Mocks;
using SFC.Tests.SensorApi;
using SFC.Tests.UserApi;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.AdminApi
{
  public class AdminApiTest
  {
    private string _url = TestHelper.GenerateUrl();
    private readonly WebApplication _app;

    public AdminApiTest()
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
          new AutofacAdminApiModule(),
          new AutofacUserApiModule(),
          new AutofacSensorApiModule(),
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
    public async void AlertNotificationsWithUserTest()
    {
      // Arrange
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      var userApi = RestClient.For<IUserApi>(_url);
      var adminApi = RestClient.For<IAdminApi>(_url);
      string confirmationId = await userApi.PostAccount(postAccountModel);
      await userApi.PostAccountConfirmation(confirmationId);
      
      adminApi.Token = userApi.Token = "Bearer " + await RestClient.For<IAuthenticationApi>(_url).Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));
      
      Guid sensorId = await userApi.PostSensor(new PostSensorModel() { ZipCode = postAccountModel.ZipCode });

      await RestClient.For<ISensorApi>(_url).PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = { { PolutionType.PM2_5, 25 } }
      });


      // Act
      var result = await adminApi.GetAlertNotificationsWithUserData(1, int.MaxValue);

      // Assert
      var entry = result.Results.FirstOrDefault(f => f.LoginName == postAccountModel.LoginName);
      Assert.NotNull(entry);
      Assert.Equal(postAccountModel.LoginName, entry.LoginName);
      Assert.Equal(1, entry.AlertsSentCount);
    }

    [Fact]
    public async void SearchableDashboardTest()
    {
      // Arrange
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      var userApi = RestClient.For<IUserApi>(_url);
      var adminApi = RestClient.For<IAdminApi>(_url);
      string confirmationId = await userApi.PostAccount(postAccountModel);
      await userApi.PostAccountConfirmation(confirmationId);

      adminApi.Token = userApi.Token = "Bearer " + await RestClient.For<IAuthenticationApi>(_url).Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      int expectedAlertsCount = 10;
      for (int i = 0; i < expectedAlertsCount; i++)
      {
        await userApi.PostAlert(new PostAlertModel() { ZipCode = Random.Shared.NextInt64(10000, 99999).ToString() });
      }

      // Act
      var result = await adminApi.GetSearchableDashboard(1, int.MaxValue, 10, 20);

      // Assert
      var entry = result.Results.FirstOrDefault(f => f.LoginName == postAccountModel.LoginName);
      Assert.NotNull(entry);
      Assert.Equal(expectedAlertsCount + 1, entry.AlertsCount);
    }
  }
}
