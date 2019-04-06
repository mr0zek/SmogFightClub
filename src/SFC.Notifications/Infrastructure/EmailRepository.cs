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
      if (GetEmail(loginName) != null)
      {
        _connection.Execute(
          @"update Notifications.Emails set email = @email where loginName = loginName",
          new { loginName = loginName.ToString(), email = email.ToString() });
      }
      else
      {
        _connection.Execute(
          @"insert into Notifications.Emails(loginName, email)
          values(@loginName, @email)",
          new {loginName = loginName.ToString(), email = email.ToString()});
      }
    }

    public Email GetEmail(LoginName loginName)
    {
      return _connection.QueryFirstOrDefault<string>(
        "select email from Notifications.Emails where loginName = @loginName",
        new {loginName = loginName.ToString()});
    }
  }
}
