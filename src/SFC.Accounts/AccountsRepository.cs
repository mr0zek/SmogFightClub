﻿using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Accounts.Features.AccountQuery;
using SFC.Accounts.Features.CreateAccount;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts
{
  class AccountsRepository : IAccountsPerspective, IAccountRepository
  {
    private readonly IDbConnection _connection;

    public AccountsRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
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
          fetch next @take rows only", new { accountQuery.Skip, accountQuery.Take }));

    }

    public void Add(LoginName loginName)
    {
      _connection.Execute("insert into Accounts.Accounts(loginName)values(@loginName)",
        new { loginName = loginName.ToString() });
    }
  }
}