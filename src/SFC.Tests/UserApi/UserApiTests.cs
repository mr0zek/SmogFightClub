using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.AuthenticationApi;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes;
using SFC.Sensors;
using SFC.SharedKernel;
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;
using SFC.Tests.Tools.Mocks;
using SFC.UserApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SFC.Tests.UserApi
{

  public class UserApiTests : TestBase
  {
    
    [Fact]
    public async void NotificationShoudBeSentAfterAlertCreation()
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
      await api.PostAlert(
        new PostAlertModel()
        {
          ZipCode = "01-102"
        });

      // Assert      
      _eventProcessorStatus.WaitIlde();

      Assert.Equal(3, TestSmtpClient.SentEmails.Count);
    }

    [Fact]
    public async void AccountCreationV1SuccessScenario()
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
      var api = RestClient.For<IApi>(_url);
      Guid confirmationId = await api.PostAccount(postAccountModel);
      await api.PostAccountConfirmation(confirmationId);

      // Assert
      _eventProcessorStatus.WaitIlde();
      Assert.Equal(2, TestSmtpClient.SentEmails.Count);

      api.Token = $"Bearer " + await RestClient.For<IApi>(_url).Login(new(postAccountModel.LoginName, postAccountModel.Password));

      var alerts = await api.GetAlerts();

      Assert.Single(alerts.Alerts!);
      Assert.Equal(postAccountModel.ZipCode, alerts.Alerts!.First().ZipCode);
    }

    [Fact]
    public async void AccountCreationV2SuccessScenario()
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
      var api = RestClient.For<IApi>(_url);
      string confirmationId = await api.PostAccountV2(postAccountModel);
      await api.PostAccountConfirmationV2(confirmationId);

      // Assert
      _eventProcessorStatus.WaitIlde();
      Thread.Sleep(1000);
      Assert.Equal(2, TestSmtpClient.SentEmails.Count);

      api.Token = $"Bearer " + await RestClient.For<IApi>(_url).Login(new(postAccountModel.LoginName, postAccountModel.Password));

      var alerts = await api.GetAlerts();

      Assert.Single(alerts.Alerts!);
      Assert.Equal(postAccountModel.ZipCode, alerts.Alerts!.First().ZipCode);
    }
  }
}
