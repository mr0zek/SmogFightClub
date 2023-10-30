using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task Add(Guid id, ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Alerts.Alerts(id, zipCode, loginName)values(@id,@zipCode,@loginName)",
        new { id, zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public async Task<bool> Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
        "select id from Alerts.Alerts where zipCode = @zipCode and loginName = @loginName",
        new { zipCode = zipCode.ToString(), loginName = loginName.ToString() })
        .Any();
    }

    public async Task<IEnumerable<Alert>> GetByZipCode(string zipCode)
    {
      return await _connection.QueryAsync<Alert>(
        "select id, zipCode, loginName from Alerts.Alerts where zipCode = @zipCode",
        new { zipCode });
    }
  }
}