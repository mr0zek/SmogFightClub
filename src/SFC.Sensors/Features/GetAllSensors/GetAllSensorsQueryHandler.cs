using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Sensors.Features.GetAllSensors
{
  internal class GetAllSensorsQueryHandler : IQueryHandler<GetAllSensorsRequest, GetAllSensorsResponse>
  {
    private readonly IDbConnection _connection;

    public GetAllSensorsQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<GetAllSensorsResponse> Handle(GetAllSensorsRequest query, CancellationToken cancellationToken)
    {
      return new GetAllSensorsResponse(await _connection.QueryAsync<GetAllSensorsResponse.SensorReadModel>(
       "select id, zipCode from Sensors.Sensors where loginName = @loginName", new { loginName = query.LoginName.ToString() }));

    }
  }
}