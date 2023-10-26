using Dapper;
using SFC.Infrastructure.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SFC.Infrastructure.Features.Communication
{
  class InboxRepository : IInbox
  {
    readonly IDbConnection _connection;
    public InboxRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public int GetLastProcessedId(string moduleName)
    {
      return _connection.QueryFirstOrDefault<int>("select top 1 lastProcessedId from inbox where moduleName = @moduleName order by id desc", new { moduleName });
    }

    public void SetProcessed(int id, string moduleName)
    {
      _connection.Execute(@"
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