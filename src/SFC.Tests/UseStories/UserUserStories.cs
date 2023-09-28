using System;
using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.Processes;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Tests.Infrastructure;
using SFC.Tests.Mocks;
using SFC.Tests.UserApi;
using SFC.UserApi;
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
      Bootstrap.Run(new string[0], new Module[]
        {
          new AutofacAdminApiModule(),
          new AutofacSensorApiModule(),
          new AutofacUserApiModule(),
          new AutofacAlertsModule(),
          new AutofacProcessesModule(),
          new AutofacNotificationsModule(),
          new AutofacSensorsModule(),
          new AutofacAccountsModule(),
          new AutofacInfrastructureModule()
        },
        builder =>
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
      string confirmationId = await RestClient.For<IUserApi>(_url).PostAccount(_postAccountModel);
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
        .Then(s=> ThenSystemSendsConfirmationEmail())
        .BDDfy();      
    }
  }
}