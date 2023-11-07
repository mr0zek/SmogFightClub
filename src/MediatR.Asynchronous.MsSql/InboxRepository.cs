using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MediatR.Asynchronous.MsSql
{
  public class InboxRepository : IInboxRepository
  {
    readonly IDbConnection _connection;
    public InboxRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public async Task<int> GetLastProcessedId(string moduleName)
    {
      int result = await _connection.QueryFirstOrDefaultAsync<int>("select top 1 lastProcessedId from inbox where moduleName = @moduleName order by id desc", new { moduleName });

      return result;
    }

    public async Task SetProcessed(int id, string moduleName)
    {
      if (id == 1)
      {
        await _connection.ExecuteAsync(@"insert into dbo.Inbox(moduleName, lastProcessedId)values(@moduleName,1)", new { moduleName });
      }
      else
      {
        int rowsAffected = await _connection.ExecuteAsync(
          @"update dbo.inbox set LastProcessedId = @id where moduleName = @moduleName and LastProcessedId = @previousId",
          new { moduleName, id, previousId = id - 1 });
        if(rowsAffected == 0)
        {
          throw new DBConcurrencyException();
        }
      }
    }
  }
}