using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{
  class OutboxRepository : IOutbox
  {
    readonly IDbConnection _connection;
    public OutboxRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task Add(EventData eventData)
    {
      await _connection.ExecuteAsync("insert into dbo.Outbox(data, type)values(@data, @type)", new { data = eventData.Data, type = eventData.Type });
    }

    public async Task<IEnumerable<EventData>> Get(int lastProcessedId, int count)
    {
      return await _connection.QueryAsync<EventData>($"select top {count} id, data, type from dbo.Outbox where id > @lastProcessedId", new { lastProcessedId });
    }
  }
}