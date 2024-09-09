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
        @"insert into dbo.Outbox(data, date, type, methodType)values(@data, @date, @type, @methodType)
          select @@identity", messageData);
    }

    public async Task<IEnumerable<MessageData>> Get(int count, string moduleName)
    {
      return await _connection.QueryAsync<MessageData>(        
        $@"select top {count} o.id, o.data, o.date, o.type, o.methodType
           from dbo.Outbox o left outer join dbo.Inbox i on o.Id = i.id
           where i.id is null");
    }
  }
}