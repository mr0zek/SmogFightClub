using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using SFC.Infrastructure.Interfaces;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  class SagaRepository : ISagaRepository
  {
    private readonly IDbConnection _connection;

    public SagaRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task Save(string id, object data)
    {
      using (var sw = new StringWriter())
      {
        JsonSerializer.CreateDefault().Serialize(sw, data);
        string strData = sw.GetStringBuilder().ToString();

        await _connection.ExecuteAsync("insert into Processes.Sagas(id, data)values(@id,@strData)",
          new { id, strData });
      }
    }

    public async Task<T?> Get<T>(string id) where T : class
    {
      string? data = await _connection.QueryFirstOrDefaultAsync<string>(
        "select data from Processes.Sagas where id = @id",
        new { id });

      if (data == null)
      {
        return null;
      }

      using (var sr = new StringReader(data))
      using (var jr = new JsonTextReader(sr))
      {
        return JsonSerializer.CreateDefault().Deserialize<T>(jr);
      }
    }
  }
}