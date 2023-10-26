using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace SFC.Infrastructure.Features.Communication
{
  class OutboxRepository : IOutbox
  {
    readonly IDbConnection _connection;
    public OutboxRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }
  
    public void Add(EventData eventData)
    {
      _connection.Execute("insert into dbo.Outbox(data, type)values(@data, @type)", new { data = eventData.Data, type = eventData.Type });
    }

    public IEnumerable<EventData> Get(int lastProcessedId, int count)
    {
      return _connection.Query<EventData>($"select top {count} id, data, type from dbo.Outbox where id > @lastProcessedId", new { lastProcessedId });
    }
  }
}