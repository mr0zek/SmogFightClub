﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Tests.Api;
using SFC.Tests.Mocks;
using SFC.Tests.UserApi;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.AuthenticationApi
{
  
  public class AuthentiacationTest : IDisposable
  {
    private string _url = TestHelper.GenerateUrl();
    private WebApplication _app;

    public AuthentiacationTest()
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
    public void LoginFailedTest()
    {
      var authApi = RestClient.For<IApi>(_url);

      Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
      {
        await authApi.Login(new CredentialsModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
      });
    }

    [Fact]
    public async void LoginSuccessTest()
    {
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      var api = RestClient.For<IApi>(_url);
      string confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      var token = await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));     

      Assert.NotNull(token);
    }

    public void Dispose()
    {
      Bootstrap.Stop(_app);
    }
  }
}
