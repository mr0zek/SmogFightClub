using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Notifications.Features.NotificationQuery;
using SFC.Notifications.Features.SendNotification;
using SFC.SharedKernel;

namespace SFC.Notifications.Infrastructure
{
  class NotificationRepository : INotificationRepository, INotificationPerspective
  {
    private readonly IDbConnection _connection;

    public NotificationRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public void Add(Email email, string title, string body, DateTime date, LoginName loginName, string notificationType)
    {
      _connection.Execute(
        @"insert into Notifications.Notifications(title, body, date, loginName,email, notificationType)
          values(@title, @body, @date, @loginName, @email, @notificationType)",
        new { title, body, date, loginName = loginName.ToString(), email = email.ToString(), notificationType });
    }

    public IEnumerable<NotificationsCountResult> GetSendNotificationsCount(string notificationType, params LoginName[] loginNames)
    {
      return _connection.Query<dynamic>(
        @"select loginName, count(*) as count from Notifications.Notifications where loginName in @loginNames and notificationType = @notificationType",
        new { loginNames = loginNames.Select(f=>f.ToString()).ToArray(), notificationType }).Select(f=> new NotificationsCountResult()
      {
        LoginName = f.loginName,
        Count = f.count
      });
    }
  }
}