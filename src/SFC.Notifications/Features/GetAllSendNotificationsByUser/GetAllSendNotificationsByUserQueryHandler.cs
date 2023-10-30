using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUser
{
  internal class GetAllSendNotificationsByUserQueryHandler : IQueryHandler<GetAllSendNotificationsByUserRequest, GetAllSendNotificationsByUserResponse>
  {
    private readonly IDbConnection _connection;

    public GetAllSendNotificationsByUserQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<GetAllSendNotificationsByUserResponse> Handle(
      GetAllSendNotificationsByUserRequest query,
      CancellationToken cancellationToken)
    {
      return new GetAllSendNotificationsByUserResponse((await _connection.QueryAsync<dynamic>(
        @"select loginName, count(*) as count from Notifications.Notifications group by loginName order by loginName offset @top rows fetch next @take rows only",
        new { top = query.Skip, take = query.Take })).Select(f => new GetAllSendNotificationsByUserResponse.SendNotification()
        {
          LoginName = f.loginName,
          Count = f.count
        }));
    }
  }
}
