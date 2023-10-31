using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Accounts.Features.GetAccountByLoginName
{
  internal class GetAccountByLoginNameQueryHandler : IQueryHandler<GetAccountByLoginNameRequest, GetAccountByLoginNameResponse>
  {
    private readonly IDbConnection _connection;

    public GetAccountByLoginNameQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<GetAccountByLoginNameResponse> Handle(GetAccountByLoginNameRequest query, CancellationToken cancellationToken)
    {
      return await _connection.QueryFirstOrDefaultAsync<GetAccountByLoginNameResponse>("select id, loginName from Accounts.Accounts where loginName = @loginName", new { loginName = query.LoginName.ToString() });
    }
  }
}
