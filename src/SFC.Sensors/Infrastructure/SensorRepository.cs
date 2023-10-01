using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.SharedKernel;

namespace SFC.Sensors.Infrastructure
{
  internal class SensorRepository : Features.RegisterMeasurement.ISensorRepository, Features.RegisterSensor.ISensorRepository
  {
    private readonly IDbConnection _connection;

    public SensorRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Guid sensorId, ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Sensors.Sensors(id, ZipCode, LoginName)values(@id, @zipCode, @loginName)",
        new { Id = sensorId, zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public bool Exits(Guid sensorId)
    {
      return _connection.QueryFirst<int>("select count(*) from Sensors.Sensors where id = @sensorId", new { sensorId }) != 0;
    }

    public Sensor Get(Guid sensorId)
    {
      return _connection.QueryFirst<Sensor>("select id, zipCode from Sensors.Sensors where id = @sensorId", new { sensorId });
    }
  }
}
