using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Infrastructure;
using Xunit;

namespace SFC.Tests
{
  //register user -> register account
  public class IntegrationTests
  {
    private const string _url = "http://localhost:5000";

    public IntegrationTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);

      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      });
    }

    [Fact]
    public async void AccountRegistrationTest()
    {
      var postAccountModel = new PostAccountModel()
      {
        LoginName = Guid.NewGuid().ToString(),
        Password = Guid.NewGuid().ToString(),
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      string confirmationId = await RestClient.For<IAccountsApi>(_url).PostAccount(postAccountModel);

      await RestClient.For<IAccountsApi>(_url).PostAccountConfirmation(confirmationId);

      Assert.Equal(2, TestSmtpClient.SentEmails.Count);
    }
  }
}

