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

    public async Task<bool> SetProcessed(int id, DateTime date, string moduleName)
    {
      try
      {
        await _connection.ExecuteAsync(@"insert into dbo.Inbox(moduleName, date, id)values(@moduleName,@date,@id)", new { id, date, moduleName });
        return true;
      }
      catch(SqlException ex) 
      {
        if(ex.Number == 2627) // Violation of PRIMARY KEY constraint 'PK_Inbox'
        {
          return false;
        }
        throw;
      }
    }
  }
}