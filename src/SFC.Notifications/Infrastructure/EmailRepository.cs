using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification;
using SFC.Notifications.Features.SetNotificationEmail;
using SFC.SharedKernel;

namespace SFC.Notifications.Infrastructure
{
  class EmailRepository : IEmailReadRepository, IEmailWriteRepository
  {
    private readonly IDbConnection _connection;

    public EmailRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }
    public async Task Set(LoginName loginName, Email email)
    {
      if (await GetEmail(loginName) != null)
      {
        await _connection.ExecuteAsync(
          @"update Notifications.Emails set email = @email where loginName = loginName",
          new { loginName = loginName.ToString(), email = email.ToString() });
      }
      else
      {
        await _connection.ExecuteAsync(
          @"insert into Notifications.Emails(loginName, email)
          values(@loginName, @email)",
          new {loginName = loginName.ToString(), email = email.ToString()});
      }
    }

    public async Task<Email> GetEmail(LoginName loginName)
    {
      return await _connection.QueryFirstOrDefaultAsync<string>(
        "select email from Notifications.Emails where loginName = @loginName",
        new {loginName = loginName.ToString()});
    }
  }
}
