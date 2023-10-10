using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Alerts.Features.CreateAlert;
using SFC.Alerts.Features.VerifySmogExceedence;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Infrastructure
{
  internal class AlertRepository : IAlertWriteRepository, IAlertReadRepository
  {
    private readonly IDbConnection _connection;

    public AlertRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public void Add(Guid id, ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Alerts.Alerts(id, zipCode, loginName)values(@id,@zipCode,@loginName)",
        new { id, zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
        "select id from Alerts.Alerts where zipCode = @zipCode and loginName = @loginName",
        new { zipCode = zipCode.ToString(), loginName = loginName.ToString() })
        .Any();
    }

    public IEnumerable<Alert> GetByZipCode(string zipCode)
    {
      return _connection.Query<Alert>(
        "select id, zipCode, loginName from Alerts.Alerts where zipCode = @zipCode",
        new { zipCode });
    }
  }
}