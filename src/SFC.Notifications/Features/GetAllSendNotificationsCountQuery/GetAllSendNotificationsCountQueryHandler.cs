using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.GetAllSendNotificationsCountQuery.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetAllSendNotificationsCountQuery
{
  internal class GetAllSendNotificationsCountQueryHandler : IQueryHandler<GetAllSendNotificationsCountRequest, IEnumerable<NotificationsCountResult>>
  {
    private readonly IDbConnection _connection;

    public GetAllSendNotificationsCountQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public IEnumerable<NotificationsCountResult> HandleQuery(GetAllSendNotificationsCountRequest query)
    {
      return _connection.Query<dynamic>(
        @"select loginName, count(*) as count from Notifications.Notifications group by loginName order by loginName offset @top rows fetch next @take rows only",
        new { top = query.Skip, take = query.Take }).Select(f => new NotificationsCountResult()
        {
          LoginName = f.loginName,
          Count = f.count
        }); ;
    }
  }
}
