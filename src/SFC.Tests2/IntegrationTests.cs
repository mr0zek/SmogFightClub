using System;
using Autofac;
using NUnit.Framework;
using RestEase;

namespace SFC.Tests
{
  //register user -> register account
  [TestFixture]
  public class IntegrationTests
  {
    private string _url = "http://localhost:5000";

    [SetUp]
    public void Setup()
    {
      Bootstrap.Run(new string[0], builder =>
      {
        builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      });
    }

    [Test]
    public async void AccountRegistration()
    {
      var postAccountModel = new PostAccountModel()
      {
        LoginName = "ala",
        ZipCode = "12-234",
        Email = "ala.ma.kotowska@gmail.com"
      };

      PostAccountResponse result = await RestClient.For<IAccountsApi>(_url).PostAccount(postAccountModel);

      string confirmationId = result.Location;

      await RestClient.For<IAccountsApi>(_url).PostAccountConfirmation(confirmationId);
    }
  }
}

