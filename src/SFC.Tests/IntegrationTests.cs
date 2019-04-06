using System;
using Autofac;
using RestEase;
using Xunit;

namespace SFC.Tests
{
  //register user -> register account
  public class IntegrationTests
  {
    private const string _url = "http://localhost:5000";

    public IntegrationTests()
    {
      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      });
    }

    [Fact]
    public async void AccountRegistration()
    {
      var postAccountModel = new PostAccountModel()
      {
        LoginName = "ala",
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      string confirmationId = await RestClient.For<IAccountsApi>(_url).PostAccount(postAccountModel);

      await RestClient.For<IAccountsApi>(_url).PostAccountConfirmation(confirmationId);
    }
  }
}

