using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SFC.Sensors.Features.GetSensor
{
  internal class GetSensorQueryHandler : IQueryHandler<GetSensorRequest, GetSensorResponse>
  {
    private readonly IDbConnection _connection;

    public GetSensorQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public GetSensorResponse HandleQuery(GetSensorRequest query)
    {
      return _connection.QueryFirst<GetSensorResponse>("select id, zipCode from Sensors.Sensors where loginName = @loginName and id = @id", new { id = query.SensorId, loginName = query.LoginName.ToString() });
    }
  }
}