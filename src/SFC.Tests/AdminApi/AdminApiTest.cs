using Autofac;
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
using SFC.SharedKernel;
using SFC.Tests.SensorApi;
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;
using SFC.Tests.UserApi;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SFC.Tests.AdminApi
{

  public class AdminApiTest : TestBase
  {
    
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

      var api = RestClient.For<IApi>(_url);
      Guid confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      api.Token = api.Token = "Bearer " + await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      Guid sensorId = await api.PostSensor(new PostSensorModel() { ZipCode = postAccountModel.ZipCode });

      await api.PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = { { PolutionType.PM2_5, 25 } }
      });

      // Act
      var result = await api.GetAlertNotificationsWithUserData(1, int.MaxValue);

      // Assert      
      var entry = result!.Results!.FirstOrDefault(f => f.LoginName! == postAccountModel.LoginName);
      Assert.NotNull(entry);
      Assert.Equal(postAccountModel.LoginName, entry.LoginName!);
      Assert.Equal(1, entry.AlertsSentCount);
    }

    [Fact]
    public async void SendNotificationsByUser()
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

      // Act
      var result = await api.GetSendNotificationsByUser(0, int.MaxValue);

      // Assert      
      Assert.Single(result.Result!);
      Assert.Equal(postAccountModel.LoginName, result.Result!.First().LoginName);
      Assert.Equal(2, result.Result!.First().Count);
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

      var api = RestClient.For<IApi>(_url);
      Guid confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      api.Token = api.Token = "Bearer " + await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      int expectedAlertsCount = 10;
      for (int i = 0; i < expectedAlertsCount; i++)
      {
        await api.PostAlert(new PostAlertModel() { ZipCode = Random.Shared.NextInt64(10000, 99999).ToString() });
      }

      // Act
      var result = await api.GetSearchableDashboard(0, int.MaxValue, 10, 20);

      // Assert      
      var entry = result.Results!.FirstOrDefault(f => f.LoginName == postAccountModel.LoginName);
      Assert.NotNull(entry);
      Assert.Equal(expectedAlertsCount + 1, entry.AlertsCount);
    }
  }
}
