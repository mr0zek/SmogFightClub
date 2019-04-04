using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Accounts.Features.CreateCount;

namespace SFC.Accounts.Features.AccountQuery
{
  class AccountsPerspective : IAccountsPerspective
  {
    private readonly IDbConnection _connection;

    public AccountsPerspective(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public AccountReadModel Get(string loginName)
    {
      return _connection.QueryFirstOrDefault<AccountReadModel>("select id, loginName from Accounts.Accounts where loginName = @loginName", new { loginName });
    }
  }
}