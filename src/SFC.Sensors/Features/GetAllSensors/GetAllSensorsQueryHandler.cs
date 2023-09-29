using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Data;
using System.Data.SqlClient;

namespace SFC.Sensors.Features.GetAllSensors
{
  internal class GetAllSensorsQueryHandler : IQueryHandler<GetAllSensorsRequest, GetAllSensorsResponse>
  {
    private readonly IDbConnection _connection;

    public GetAllSensorsQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public GetAllSensorsResponse HandleQuery(GetAllSensorsRequest query)
    {
      return new GetAllSensorsResponse(_connection.Query<GetAllSensorsResponse.SensorReadModel>(
       "select id, zipCode from Sensors.Sensors where loginName = @loginName", new { loginName = query.LoginName.ToString() }));

    }
  }
}