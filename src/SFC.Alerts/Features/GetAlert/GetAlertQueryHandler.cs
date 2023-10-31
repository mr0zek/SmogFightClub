using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

    public async Task<GetAlertResponse> Handle(GetAlertRequest query, CancellationToken cancellationToken)
    {
      return await _connection.QueryFirstAsync<GetAlertResponse>("select id,ZipCode from Alerts.Alerts where loginName = @loginName and id = @id", new { id = query.Id, loginName = query.LoginName.ToString() });
    }
  }
}
