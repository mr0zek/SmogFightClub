using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.Processes;
using SFC.Sensors;
using SFC.Tests.Api;
using SFC.Tests.Mocks;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.UserApi
{
  public class AlertsApiTests : IDisposable
  {
    private readonly string _url = TestHelper.GenerateUrl();
    private readonly WebApplication _app;

    public AlertsApiTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DBReset.ResetDatabase.Reset(connectionString);

      SFC.Infrastructure.DbMigrations.Run(connectionString);

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(Array.Empty<string>(), _url, new Module[]
        {
          new AuthenticationApiModule(),
          new UserApiModule(),
          new AccountsModule(),
          new SensorsModule(),
          new AlertsModule(),
          new ProcessesModule(),
          new NotificationsModule(),
          new InfrastructureModule()
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
    public async void AddGetGetAllTest()
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

      Guid confirmationId = await RestClient.For<IApi>(_url).PostAccount(postAccountModel);
      await RestClient.For<IApi>(_url).PostAccountConfirmation(confirmationId);

      api.Token = "Bearer " + await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      await api.PostUser(new PostUserModel("noreply@example.com"));

      // Act
      var alert = new PostAlertModel()
      {
        ZipCode = "01-102"
      };

      var id = await api.PostAlert(alert);

      // Assert
      var alert2 = await api.GetAlert(id);
      Assert.Equal(alert.ZipCode, alert2.ZipCode);
    }
  }
}
