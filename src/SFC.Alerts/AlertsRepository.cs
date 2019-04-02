using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Alerts.Contract;
using SFC.Alerts.Contract.Query;
using SFC.SharedKernel;

namespace SFC.Alerts
{
  class AlertsRepository : IAlertsPerspective, IAlertsRepository
  {
    private readonly IDbConnection _connection;

    public AlertsRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public AlertsReadModel GetAll(LoginName loginName)
    {
      return new AlertsReadModel(_connection.Query<AlertReadModel>(
        "select id, ZipCode from Alerts.Alerts where loginName = @loginName", new { loginName }));
    }

    public AlertReadModel Get(string id, LoginName loginName)
    {
      return _connection.QueryFirst<AlertReadModel>("select id,ZipCode from Alerts.Alerts where loginName = @loginName nad id = @id", new { id, loginName });
    }

    public void Add(ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Alerts.Alerts(zipCode, loginName)values(@zipCode,@loginName)",
        new {zipCode, loginName});
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
        "select id from Alerts where zipCode = @zipCode and loginName = @loginName",
        new { zipCode, loginName })
        .Any();
    }
  }
}