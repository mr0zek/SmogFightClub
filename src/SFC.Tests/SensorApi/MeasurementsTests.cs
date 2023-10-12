﻿using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Tests.Mocks;
using System;
using System.Collections.Generic;
using Xunit;
using SFC.Tests.UserApi;
using SFC.UserApi;
using SFC.SharedKernel;
using SFC.AuthenticationApi;
using Microsoft.AspNetCore.Builder;
using SFC.Tests.Api;
using SFC.Processes;

namespace SFC.Tests.SensorApi
{

    public class MeasurementsTests : IDisposable
  {
    private readonly string _url = TestHelper.GenerateUrl();
    private readonly WebApplication _app;

    public MeasurementsTests()
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
          new AccountsModule(),
          new AlertsModule(),
          new NotificationsModule(),
          new UserApiModule(),
          new ProcessesModule(),
          new SensorApiModule(),          
          new SensorsModule(),          
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
    public async void PostMeasurements_should_return_ok()
    {
      // Arrange      
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      var api = RestClient.For<IApi>(_url);
      Guid confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      api.Token = api.Token = "Bearer " + await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));
      
      Guid sensorId = await api.PostSensor(new PostSensorModel() { ZipCode = "01-102"});

      // Act, Assert
      await api.PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = new Dictionary<PolutionType, decimal>() { { PolutionType.PM2_5, 12 }, { PolutionType.NO2, 34 } }
      });
    }
  }
}
