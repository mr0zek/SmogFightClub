using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MediatR.Asynchronous.MsSql
{

  public class OutboxRepository : IOutboxRepository
  {
    readonly IDbConnection _connection;
    public OutboxRepository(Configuration configuration)
    {
      _connection = new SqlConnection(configuration.ConnectionString.ToString());
    }

    public async Task Add(MessageData messageData)
    {
      await _connection.ExecuteAsync("insert into dbo.Outbox(data, type, methodType)values(@data, @type, @methodType)", new { data = messageData.Data, type = messageData.Type, methodType = messageData.MethodType });
    }

    public async Task<IEnumerable<MessageData>> Get(int lastProcessedId, int count)
    {
      return await _connection.QueryAsync<MessageData>($"select top {count} id, data, type, methodType from dbo.Outbox where id > @lastProcessedId", new { lastProcessedId });
    }
  }
}