using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Tests.Mocks;
using System;
using System.Collections.Generic;
using Xunit;
using SFC.Tests.UserApi;
using SFC.UserApi;
using SFC.SharedKernel;
using SFC.AuthenticationApi;
using Microsoft.AspNetCore.Builder;

namespace SFC.Tests.SensorApi
{
  
  public class MeasurementsTests : IDisposable
  {
    private string _url = TestHelper.GenerateUrl();
    private WebApplication _app;

    public MeasurementsTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      SFC.Infrastructure.DbMigrations.Run(connectionString);      

      TestSmtpClient.Clear();
      _app = Bootstrap.Run(new string[0],_url, new Module[]
        {
          new AutofacAuthenticationApiModule(),
          new AutofacAccountsModule(),
          new AutofacUserApiModule(),
          new AutofacSensorApiModule(),          
          new AutofacSensorsModule(),          
          new AutofacInfrastructureModule()
        },
        builder =>
        {
          builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();          
        });
    }

    public void Dispose()
    {
      Bootstrap.Stop(_app);
    }

    [Fact]
    public async void PostMeasurements_should_return_ok()
    {
      // Arrange      
      var userApi = RestClient.For<IUserApi>(_url);
      userApi.Token = "Bearer " + await RestClient.For<IAuthenticationApi>(_url).Login(new CredentialsModel("admin", "password"));

      Guid sensorId = await userApi.PostSensor(new PostSensorModel() { ZipCode = "01-102"});

      // Act, Assert
      await RestClient.For<ISensorApi>(_url).PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = new Dictionary<PolutionType, decimal>() { { PolutionType.PM2_5, 12 }, { PolutionType.NO2, 34 } }
      });
    }
  }
}
