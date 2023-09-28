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

namespace SFC.Tests.SensorApi
{
    public class MeasurementsTests
  {
    private const string _url = "http://localhost:5000";
    
    public MeasurementsTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);      

      TestSmtpClient.Clear();
      Bootstrap.Run(new string[0], new Module[]
        {
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
        
    [Fact]
    public async void PostMeasurements_should_return_ok()
    {
      Guid sensorId = await RestClient.For<IUserApi>(_url).PostSensor(new PostSensorModel() { ZipCode = "01-102"});


      await RestClient.For<ISensorApi>(_url).PostMeasurements(sensorId, new PostMeasurementModel()
      {
        Values = new Dictionary<string, decimal>() { { "a", 12 }, { "b", 34 } }
      });

    }
  }
}
