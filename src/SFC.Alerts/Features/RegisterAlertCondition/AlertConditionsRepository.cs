using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition
{
  internal class AlertRepository : IAlertRepository
  {
    private readonly IDbConnection _connection;

    public AlertRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
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