using Dapper;
using SFC.Alerts.Features.GetAlert;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.GetAllAlerts
{
  internal class GetAllAlertsQueryHandler : IQueryHandler<GetAllAlertsRequest, GetAllAlertsResponse>
    {
        private readonly IDbConnection _connection;

        public GetAllAlertsQueryHandler(ConnectionString connectionString)
        {
            _connection = new SqlConnection(connectionString.ToString());
        }

        public async Task<GetAllAlertsResponse> Handle(GetAllAlertsRequest query, CancellationToken cancellationToken)
        {
            return new GetAllAlertsResponse(await _connection.QueryAsync<AlertResponse>(
              "select id, ZipCode from Alerts.Alerts where loginName = @loginName", new { loginName = query.LoginName.ToString() }));

        }
    }
}
