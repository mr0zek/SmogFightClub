using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SFC.Accounts.Features.GetAccountByLoginName
{
  internal class GetAccountByLoginNameQueryHandler : IQueryHandler<GetAccountByLoginNameRequest, GetAccountByLoginNameResponse>
  {
    private readonly IDbConnection _connection;

    public GetAccountByLoginNameQueryHandler(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public GetAccountByLoginNameResponse HandleQuery(GetAccountByLoginNameRequest query)
    {
      return _connection.QueryFirstOrDefault<GetAccountByLoginNameResponse>("select id, loginName from Accounts.Accounts where loginName = @loginName", new { loginName = query.LoginName.ToString() });
    }
  }
}
