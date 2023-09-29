using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.GetSendNotificationsCount.Contract;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetSendNotificationsCount
{
  internal class GetSendNotificationsCountQueryHandler : IQueryHandler<GetSendNotificationsCountRequest, IEnumerable<GetSendNotificationsCountResponse>>
  {
    private readonly IDbConnection _connection;

    public GetSendNotificationsCountQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public IEnumerable<GetSendNotificationsCountResponse> HandleQuery(GetSendNotificationsCountRequest request)
    {
      return _connection.Query<dynamic>(
        @"select loginName, count(*) as count from Notifications.Notifications where loginName in @loginNames and notificationType = @notificationType group by loginName",
        new { loginNames = request.LoginNames.Select(f => f.ToString()).ToArray(), request.NotificationType }).Select(f => new GetSendNotificationsCountResponse()
        {
          LoginName = f.loginName,
          Count = f.count
        });
    }
  }
}
