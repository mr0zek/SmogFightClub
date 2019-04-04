using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Notifications.Features.SendNotification;
using SFC.Notifications.Features.SetNotificationEmail;
using SFC.SharedKernel;

namespace SFC.Notifications.Infrastructure
{
  class EmailRepository : IEmailReadRepository, IEmailWriteRepository
  {
    private readonly IDbConnection _connection;

    public EmailRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }
    public void Set(LoginName loginName, Email email)
    {
      _connection.Execute(
        @"insert into Notifications.Notifications(title, body, date, loginName,email)
          values(@title, @body, @date, @loginName, @email)",
        new { loginName, email });
    }

    public Email GetEmail(LoginName loginName)
    {
      return _connection.QueryFirst<string>(
        "select email from Notifications.Notifications where loginName = @loginName",
        new {loginName});
    }
  }
}
