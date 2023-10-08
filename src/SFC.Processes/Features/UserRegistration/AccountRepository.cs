using Dapper;
using SFC.Infrastructure.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace SFC.Processes.Features.UserRegistration
{
  class AccountRepository : IAccountRepository
  {
    private readonly IDbConnection _connection;

    public AccountRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Account account)
    {
      _connection.Execute("insert into Processes.Accounts(id, email, zipCode, loginName, passwordHash)values(@id, @email, @zipCode, @loginName, @passwordHash)",
          new { 
            id = account.Id, 
            email = account.Email.ToString(), 
            zipCode = account.ZipCode.ToString(),
            loginName = account.LoginName.ToString(), 
            passwordHash = account.PasswordHash.Value });
    }

    public Account Get(string id)
    {
      return _connection.QueryFirstOrDefault<Account>(
        "select id, email, zipCode, loginName, passwordHash from Processes.Accounts where id = @id",
        new { id });
    }
  }
}