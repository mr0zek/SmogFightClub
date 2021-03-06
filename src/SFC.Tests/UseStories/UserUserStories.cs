﻿using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Infrastructure;
using SFC.Tests.Infrastructure;
using SFC.Tests.UseStories.Mocks;
using SFC.Tests.UseStories.UserApi;
using TestStack.BDDfy;
using Xunit;

namespace SFC.Tests.UseStories
{
  public class UserUserStories : IClassFixture<UserStoriesFixture>
  {
    private const string _url = "http://localhost:5000";
    private PostAccountModel _postAccountModel;
   
    [Given]
    void GivenSystemWithNotRegisteredAccount()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);

      TestSmtpClient.Clear();
      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      });

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
      string confirmationId = await RestClient.For<IAccountsApi>(_url).PostAccount(_postAccountModel);
    }

    void ThenSystemSendsConfirmationEmail()
    {
      Assert.Single(TestSmtpClient.SentEmails);
    }

    [Fact]
    public void NewUserRegistration()
    {
      this.Given(s => s.GivenSystemWithNotRegisteredAccount())
        .When(s => s.WhenUserPostRegistrationForm())
        .Then(s=>ThenSystemSendsConfirmationEmail())
        .BDDfy();      
    }
  }
}