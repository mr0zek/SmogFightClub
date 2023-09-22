using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Contract;

namespace SFC.Sensors.Features.RegisterMeasurement
{

  internal class MeasurementRepository : IMeasurementRepository
  {
    private readonly IDbConnection _connection;

    public MeasurementRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Guid sensorId, DateTime date, ElementName elementName, decimal elementValue)
    {
      _connection.Execute("insert into Sensors.Measurements(sensorId, date, elementName, elementValue)values(@sensorId, @date, @elementName, @elementValue)",
        new { sensorId, date, elementName, elementValue});
    }
  }
}