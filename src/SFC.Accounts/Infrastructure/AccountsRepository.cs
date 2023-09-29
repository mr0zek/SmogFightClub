using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Accounts.Features.CreateAccount;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts.Infrastructure
{
    class AccountsRepository : IAccountRepository
    {
        private readonly IDbConnection _connection;

        public AccountsRepository(ConnectionString connectionString)
        {
            _connection = new SqlConnection(connectionString.ToString());
        }

        public void Add(LoginName loginName)
        {
            _connection.Execute("insert into Accounts.Accounts(loginName)values(@loginName)",
              new { loginName = loginName.ToString() });
        }
    }
}