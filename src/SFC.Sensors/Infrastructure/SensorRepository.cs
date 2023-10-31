using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

    public async Task Add(Guid sensorId, ZipCode zipCode, LoginName loginName)
    {
      await _connection.ExecuteAsync("insert into Sensors.Sensors(id, ZipCode, LoginName)values(@id, @zipCode, @loginName)",
        new { Id = sensorId, zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public async Task<bool> Exits(Guid sensorId)
    {
      return (await _connection.QueryFirstAsync<int>("select count(*) from Sensors.Sensors where id = @sensorId", new { sensorId })) != 0;
    }

    public async Task<Sensor> Get(Guid sensorId)
    {
      return await _connection.QueryFirstAsync<Sensor>("select id, zipCode from Sensors.Sensors where id = @sensorId", new { sensorId });
    }
  }
}
