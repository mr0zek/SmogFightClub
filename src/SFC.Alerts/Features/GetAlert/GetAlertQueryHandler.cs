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

namespace SFC.Alerts.Features.GetAlert
{
  internal class GetAlertQueryHandler : IQueryHandler<GetAlertRequest, GetAlertResponse>
  {
    private readonly IDbConnection _connection;

    public GetAlertQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public GetAlertResponse HandleQuery(GetAlertRequest query)
    {
      return _connection.QueryFirst<GetAlertResponse>("select id,ZipCode from Alerts.Alerts where loginName = @loginName and id = @id", new { id = query.Id, loginName = query.LoginName.ToString() });
    }
  }
}
