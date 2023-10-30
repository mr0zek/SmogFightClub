using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

    public async Task Add(LoginName loginName, PasswordHash passwordHash)
    {
      await _connection.ExecuteAsync("insert into Accounts.Accounts(loginName, passwordHash)values(@loginName, @passwordHash)",
        new { loginName = loginName.ToString(), passwordHash = passwordHash.Value });
    }

    public async Task<bool> Authenticate(LoginName loginName, PasswordHash hash)
    {
      return await _connection.QueryFirstOrDefaultAsync<int>("select count(*) from Accounts.Accounts where loginName = @loginName and passwordHash = @passwordHash",
        new { loginName = loginName.ToString(), passwordHash = hash.Value }) != 0;
    }
  }
}