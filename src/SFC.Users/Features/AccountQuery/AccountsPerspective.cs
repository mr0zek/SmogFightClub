using System.Data;
using System.Data.SqlClient;
using Dapper;

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

    public AccountsReadModel Search(AccountQuery accountQuery)
    {
      return new AccountsReadModel(_connection.Query<AccountReadModel>(
        @"select id, loginName 
          from Accounts.Accounts 
          order by id 
          offset @skip rows 
          fetch next @take rows only", new { accountQuery.Skip, accountQuery.Take}));

    }
  }
}