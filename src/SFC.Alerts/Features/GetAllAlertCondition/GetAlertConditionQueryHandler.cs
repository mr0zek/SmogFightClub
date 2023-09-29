using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.GetAllAlertCondition
{
  internal class GetAlertConditionQueryHandler : IQueryHandler<GetAlertConditionRequest, GetAlertConditionResponse>
  {
    private IDbConnection _connection;

    public GetAlertConditionQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public GetAlertConditionResponse HandleQuery(GetAlertConditionRequest query)
    {
      return _connection.QueryFirst<GetAlertConditionResponse>("select id,ZipCode from Alerts.Alerts where loginName = @loginName nad id = @id", new { id = query.Id, loginName = query.LoginName.ToString() });
    }
  }
}
