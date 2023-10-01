using Dapper;
using SFC.Alerts.Features.GetAllAlert;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Data;
using System.Data.SqlClient;

namespace SFC.Alerts.Features.GetAllAlerts
{
  internal class GetAllAlertsQueryHandler : IQueryHandler<GetAllAlertsRequest, GetAllAlertsResponse>
    {
        private IDbConnection _connection;

        public GetAllAlertsQueryHandler(ConnectionString connectionString)
        {
            _connection = new SqlConnection(connectionString.ToString());
        }

        public GetAllAlertsResponse HandleQuery(GetAllAlertsRequest query)
        {
            return new GetAllAlertsResponse(_connection.Query<AlertResponse>(
              "select id, ZipCode from Alerts.Alerts where loginName = @loginName", new { loginName = query.LoginName.ToString() }));

        }
    }
}
