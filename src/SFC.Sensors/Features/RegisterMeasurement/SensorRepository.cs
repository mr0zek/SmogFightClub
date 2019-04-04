using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  class SensorRepository : ISensorRepository
  {
    private readonly IDbConnection _connection;

    public SensorRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public bool Exits(Guid sensorId)
    {
      return _connection.QueryFirst<int>("select count(*) from Sensors.Sensors where sensorId = @sensorId", new {sensorId}) != 0;
    }
  }
}