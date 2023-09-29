using Dapper;
using SFC.Alerts.Features.GetAllAlertCondition;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Data;
using System.Data.SqlClient;

namespace SFC.Alerts.Features.GetAllAlertConditions
{
  internal class GetAllAlertConditionsQueryHandler : IQueryHandler<GetAllAlertConditionsRequest, GetAllAlertConditionsResponse>
    {
        private IDbConnection _connection;

        public GetAllAlertConditionsQueryHandler(ConnectionString connectionString)
        {
            _connection = new SqlConnection(connectionString.ToString());
        }

        public GetAllAlertConditionsResponse HandleQuery(GetAllAlertConditionsRequest query)
        {
            return new GetAllAlertConditionsResponse(_connection.Query<AlertConditionResponse>(
              "select id, ZipCode from Alerts.Alerts where loginName = @loginName", new { loginName = query.LoginName.ToString() }));

        }
    }
}
