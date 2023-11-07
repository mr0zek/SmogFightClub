using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Sensors.Features.RegisterMeasurement;
using System;
using System.Collections.Generic;
using Xunit;
using SFC.Tests.UserApi;
using SFC.UserApi;
using SFC.SharedKernel;
using SFC.AuthenticationApi;
using Microsoft.AspNetCore.Builder;
using SFC.Processes;
using Xunit.Abstractions;
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;

namespace SFC.Tests.SensorApi
{

    public class MeasurementsTests : TestBase
  {
    
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

      Guid sensorId = await api.PostSensor(new PostSensorModel() { ZipCode = "01-102" });

      // Act, Assert
      await api.PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = new Dictionary<PolutionType, decimal>() { { PolutionType.PM2_5, 12 }, { PolutionType.NO2, 34 } }
      });
    }
  }
}
