﻿using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Features.Database;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Tests.Tools;
using SFC.Tests.Tools.Api;
using SFC.Tests.Tools.DBReset;
using SFC.Tests.Tools.Infrastructure;
using SFC.Tests.Tools.Mocks;
using SFC.UserApi;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace SFC.Tests.UseStories
{
    public class UserStories : TestBase, IClassFixture<UserStoriesFixture>, IDisposable
  {
    private PostAccountModel? _postAccountModel;
        
    [Given]
    void GivenSystemWithNotRegisteredAccount()
    {            
      _postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };
    }

    async void WhenUserPostRegistrationForm()
    {
      await RestClient.For<IApi>(_url).PostAccount(_postAccountModel!);
    }

    void ThenSystemSendsConfirmationEmail()
    {
      _eventProcessorStatus.WaitIlde();
      Assert.Single(TestSmtpClient.SentEmails);
    }

    [Fact]
    public void NewUserRegistration()
    {
      Configurator.BatchProcessors.HtmlReport.Enable();

      this.Given(s => s.GivenSystemWithNotRegisteredAccount())        
        .When(s => s.WhenUserPostRegistrationForm())
        .Then(s => ThenSystemSendsConfirmationEmail())
        .BDDfy();
    }

    public void t()
    {
      //Given().System
      //  .WithRegisteredAccoun(f => f.Login = "test")
      //  .WithRegisteredSensor(f => f.ZipCode = "12-234")
      //  .WithRegisteredAlarm(f => f.ZipCode = "12-233");

      //When().User(f => f.LoginName = "test")
      //  .AddsSensor()
      //  .CreatesAlarm();

      //Assert()
      //  .User(f => f.LoginName = "test")
      //    .HasSensor(f => f.ZipCode)
      //    .HasAlert(f => f.ZipCode);
    }
  }
}