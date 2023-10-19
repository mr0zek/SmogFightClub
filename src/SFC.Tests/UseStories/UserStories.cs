using System;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.AdminApi;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Infrastructure.Features.Database;
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

namespace SFC.Tests.UseStories
{
    public class UserStories : IClassFixture<UserStoriesFixture>, IDisposable
  {
    private readonly string _url = TestHelper.GenerateUrl();
    private PostAccountModel _postAccountModel;
    private WebApplication _app;

    [Given]
    void GivenSystemWithNotRegisteredAccount()
    {            
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      ResetDatabase.Reset(connectionString);      

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(Array.Empty<string>(), _url, new Module[]
        {
          new AdminApiModule(),
          new SensorApiModule(),
          new UserApiModule(),
          new AlertsModule(),
          new ProcessesModule(),
          new NotificationsModule(),
          new SensorsModule(),
          new AccountsModule(),
          new InfrastructureModule()
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

    public void Dispose()
    {
      Bootstrap.Stop(_app);
    }

    async void WhenUserPostRegistrationForm()
    {
      await RestClient.For<IApi>(_url).PostAccount(_postAccountModel);
    }

    void ThenSystemSendsConfirmationEmail()
    {
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