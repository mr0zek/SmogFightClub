using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUser
{
  internal class GetAllSendNotificationsByUserQueryHandler : IQueryHandler<GetAllSendNotificationsByUserRequest, IEnumerable<GetAllSendNotificationsByUserResponse>>
  {
    private readonly IDbConnection _connection;

    public GetAllSendNotificationsByUserQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public IEnumerable<GetAllSendNotificationsByUserResponse> HandleQuery(GetAllSendNotificationsByUserRequest query)
    {
      return _connection.Query<dynamic>(
        @"select loginName, count(*) as count from Notifications.Notifications group by loginName order by loginName offset @top rows fetch next @take rows only",
        new { top = query.Skip, take = query.Take }).Select(f => new GetAllSendNotificationsByUserResponse()
        {
          LoginName = f.loginName,
          Count = f.count
        }); ;
    }
  }
}
