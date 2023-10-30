using Dapper;
using SFC.Infrastructure.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SFC.Processes.Features.UserRegistration
{
  class AccountRepository : IAccountRepository
  {
    private readonly IDbConnection _connection;

    public AccountRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task Add(Account account)
    {
      await _connection.ExecuteAsync("insert into Processes.Accounts(id, email, zipCode, loginName, passwordHash)values(@id, @email, @zipCode, @loginName, @passwordHash)",
          new { 
            id = account.Id, 
            email = account.Email.ToString(), 
            zipCode = account.ZipCode.ToString(),
            loginName = account.LoginName.ToString(), 
            passwordHash = account.PasswordHash });
    }

    public async Task<Account> Get(Guid id)
    {
      return await _connection.QueryFirstOrDefaultAsync<Account>(
        "select id, email, zipCode, loginName, passwordHash from Processes.Accounts where id = @id",
        new { id });
    }
  }
}