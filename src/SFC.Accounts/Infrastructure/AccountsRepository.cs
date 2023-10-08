using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Accounts.Features.Authenticate;
using SFC.Accounts.Features.CreateAccount;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts.Infrastructure
{
  class AccountsRepository : IAccountRepository, IAuthenticationRepository
  {
    private readonly IDbConnection _connection;

    public AccountsRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(LoginName loginName, PasswordHash passwordHash)
    {
      _connection.Execute("insert into Accounts.Accounts(loginName, passwordHash)values(@loginName, @passwordHash)",
        new { loginName = loginName.ToString(), passwordHash = passwordHash.Value });
    }

    public bool Authenticate(LoginName loginName, PasswordHash hash)
    {
      return _connection.QueryFirstOrDefault<int>("select count(*) from Accounts.Accounts where loginName = @loginName and passwordHash = @passwordHash",
        new { loginName = loginName.ToString(), passwordHash = hash.Value }) != 0;
    }
  }
}