﻿using System.Data;
using System.Data.SqlClient;
using System.IO;
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

    public void Save(string id, object data)
    {
      using (var sw = new StringWriter())
      {
        JsonSerializer.CreateDefault().Serialize(sw, data);
        string strData = sw.GetStringBuilder().ToString();

        _connection.Execute("insert into Processes.Sagas(id, data)values(@id,@strData)",
          new { id, strData });
      }
    }

    public T Get<T>(string id) where T : class
    {
      string data = _connection.QueryFirstOrDefault<string>(
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