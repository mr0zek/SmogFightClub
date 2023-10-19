using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Infrastructure.Features.Tracing;
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
  public class AlertsApiTests : TestBase
  {
    public AlertsApiTests(ITestOutputHelper output) : base(output)
    {
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
