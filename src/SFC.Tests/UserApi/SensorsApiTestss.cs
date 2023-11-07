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
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SFC.Tests.UserApi
{
    public class SensorsApiTests : TestBase
  {
    
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
      var sensor = new PostSensorModel()
      {
        ZipCode = "01-102"
      };

      var id = await api.PostSensor(sensor);

      // Assert
      var sensor2 = await api.GetSensor(id);
      Assert.Equal(sensor.ZipCode, sensor2.ZipCode);

      var allSensors = await api.GetAllSensors();
      Assert.Single(allSensors.Sensors!);
      Assert.Contains(allSensors.Sensors!, f =>f.ZipCode == sensor.ZipCode);
    }

    [Fact]
    public async void CheckValidation()
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
      var sensor = new PostSensorModel();

      await Assert.ThrowsAsync<ApiException>(async () => await api.PostSensor(sensor));
    }
  }
}
