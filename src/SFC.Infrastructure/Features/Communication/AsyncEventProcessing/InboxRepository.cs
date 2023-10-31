using Dapper;
using SFC.Infrastructure.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{
  class InboxRepository : IInbox
  {
    readonly IDbConnection _connection;
    public InboxRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task<int> GetLastProcessedId(string moduleName)
    {
      return await _connection.QueryFirstOrDefaultAsync<int>("select top 1 lastProcessedId from inbox where moduleName = @moduleName order by id desc", new { moduleName });
    }

    public async Task SetProcessed(int id, string moduleName)
    {
      await _connection.ExecuteAsync(@"
        if exists(select 1 from dbo.inbox where moduleName = @moduleName) 
        begin
          update dbo.inbox set LastProcessedId = @id where moduleName = @moduleName
        end        
        else
        begin
          insert into dbo.Inbox(moduleName, lastProcessedId)values(@moduleName,@id) 
        end", new { moduleName, id });
    }
  }
}