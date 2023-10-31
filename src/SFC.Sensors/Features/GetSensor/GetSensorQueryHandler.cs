using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Sensors.Features.GetSensor
{
  internal class GetSensorQueryHandler : IQueryHandler<GetSensorRequest, GetSensorResponse>
  {
    private readonly IDbConnection _connection;

    public GetSensorQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<GetSensorResponse> Handle(GetSensorRequest query, CancellationToken cancellationToken)
    {
      return await _connection.QueryFirstAsync<GetSensorResponse>("select id, zipCode from Sensors.Sensors where loginName = @loginName and id = @id", new { id = query.SensorId, loginName = query.LoginName.ToString() });
    }
  }
}