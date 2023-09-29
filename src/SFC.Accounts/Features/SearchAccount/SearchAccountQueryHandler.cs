using Dapper;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SFC.Accounts.Features.SearchAccount
{
  internal class SearchAccountQueryHandler : IQueryHandler<SearchAccountRequest, SearchAccountResponse>
  {
    private readonly IDbConnection _connection;

    public SearchAccountQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public SearchAccountResponse HandleQuery(SearchAccountRequest query)
    {
      return new SearchAccountResponse(_connection.Query<SearchAccountResponse.Account>(
        @"select id, loginName 
          from Accounts.Accounts 
          order by id 
          offset @skip rows 
          fetch next @take rows only", new { skip = query.Skip, take = query.Take }));
    }
  }
}