using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.Users.Contract.Query;

namespace SFC.Users
{
  class UsersPerspective : IUsersPerspective
  {
    private readonly IDbConnection _connection;

    public UsersPerspective(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public User Get(string loginName)
    {
      return _connection.QueryFirstOrDefault<User>("select id, loginName from Users.Users where loginName = @loginName", new { loginName });
    }
  }
}