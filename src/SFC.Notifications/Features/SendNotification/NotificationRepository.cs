using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  class NotificationRepository : INotificationRepository
  {
    private readonly IDbConnection _connection;

    public NotificationRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public void Add(Email email, string title, string body, DateTime date, LoginName loginName)
    {
      _connection.Execute(
        @"insert into Notifications.Notifications(title, body, date, loginName,email)
          values(@title, @body, @date, @loginName, @email)",
        new { title, body, date, loginName, email });
    }
  }
}