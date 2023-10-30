using Dapper;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Accounts.Features.SearchAccount
{
  internal class SearchAccountQueryHandler : IQueryHandler<SearchAccountRequest, SearchAccountResponse>
  {
    private readonly IDbConnection _connection;

    public SearchAccountQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<SearchAccountResponse> Handle(SearchAccountRequest query, CancellationToken cancellationToken)
    {
      return new SearchAccountResponse(await _connection.QueryAsync<SearchAccountResponse.Account>(
        @"select id, loginName 
          from Accounts.Accounts 
          order by id 
          offset @skip rows 
          fetch next @take rows only", new { skip = query.Skip, take = query.Take }));
    }
  }
}