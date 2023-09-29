using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification;
using SFC.SharedKernel;

namespace SFC.Notifications.Infrastructure
{
    class NotificationRepository : INotificationRepository
  {
    private readonly IDbConnection _connection;

    public NotificationRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Email email, string title, string body, DateTime date, LoginName loginName, string notificationType)
    {
      _connection.Execute(
        @"insert into Notifications.Notifications(title, body, date, loginName,email, notificationType)
          values(@title, @body, @date, @loginName, @email, @notificationType)",
        new { title, body, date, loginName = loginName.ToString(), email = email.ToString(), notificationType });
    }    
  }
}