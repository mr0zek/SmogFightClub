using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MediatR.Asynchronous.MsSql
{

  public class OutboxRepository : IOutboxRepository
  {
    readonly IDbConnection _connection;
    public OutboxRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public async Task Add(MessageData messageData)
    {
      messageData.Id = await _connection.QueryFirstAsync<int>(
        @"insert into dbo.Outbox(data, type, methodType)values(@data, @type, @methodType)
          select @@identity", messageData);
    }

    public async Task<IEnumerable<MessageData>> Get(int lastProcessedId, int count)
    {
      return await _connection.QueryAsync<MessageData>($"select top {count} id, data, type, methodType from dbo.Outbox where id > @lastProcessedId", new { lastProcessedId });
    }
  }
}