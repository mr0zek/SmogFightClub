using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.SharedKernel;
using static SFC.Sensors.Features.RegisterMeasurement.Contract.RegisterMeasurementCommand;

namespace SFC.Sensors.Features.RegisterMeasurement
{

  internal class MeasurementRepository : IMeasurementRepository
  {
    private readonly IDbConnection _connection;

    public MeasurementRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Guid sensorId, DateTime date, PolutionType polutionType, decimal value)
    {
      _connection.Execute("insert into Sensors.Measurements(sensorId, date, elementName, elementValue)values(@sensorId, @date, @polutionType, @value)",
        new { sensorId, date, polutionType = polutionType.ToString(), value});
    }
  }
}