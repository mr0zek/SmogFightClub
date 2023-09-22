using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Alerts.Features.AlertQuery;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition
{
  internal class AlertConditionConditionConditionsRepository : IAlertConditionsPerspective, IAlertConditionsRepository
  {
    private readonly IDbConnection _connection;

    public AlertConditionConditionConditionsRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public AlertsReadModel GetAll(LoginName loginName)
    {
      return new AlertsReadModel(_connection.Query<AlertReadModel>(
        "select id, ZipCode from Alerts.Alerts where loginName = @loginName", new { loginName = loginName.ToString() }));
    }

    public AlertReadModel Get(string id, LoginName loginName)
    {
      return _connection.QueryFirst<AlertReadModel>("select id,ZipCode from Alerts.Alerts where loginName = @loginName nad id = @id", new { id, loginName = loginName.ToString() });
    }

    public void Add(ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Alerts.Alerts(zipCode, loginName)values(@zipCode,@loginName)",
        new {zipCode = zipCode.ToString(), loginName = loginName.ToString()});
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
        "select id from Alerts.Alerts where zipCode = @zipCode and loginName = @loginName",
        new { zipCode = zipCode.ToString(), loginName = loginName.ToString() })
        .Any();
    }
  }
}