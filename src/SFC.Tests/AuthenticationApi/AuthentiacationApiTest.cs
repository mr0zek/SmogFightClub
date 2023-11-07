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
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;
using SFC.Tests.UserApi;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SFC.Tests.AuthenticationApi
{

  public class AuthentiacationApiTest : TestBase
  {
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
      Guid confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      var token = await api.Login(new CredentialsModel(postAccountModel.LoginName, postAccountModel.Password));

      Assert.NotNull(token);
    }
  }
}
