using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Sensors.Contract.Query;
using SFC.SharedKernel;

namespace SFC.Sensors
{
  class SensorsRepository : ISensorsPerspective
  {
    private readonly IDbConnection _connection;

    public SensorsRepository(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public SensorsReadModel GetAll(LoginName loginName)
    {
      return new SensorsReadModel(_connection.Query<SensorReadModel>(
        "select id, ZipCode from Sensors.Sensors where loginName = @loginName", new { loginName }));
    }

    public SensorReadModel Get(string id, LoginName loginName)
    {
      return _connection.QueryFirst<SensorReadModel>("select id,ZipCode from Sensors.Sensors where loginName = @loginName nad id = @id", new { id, loginName });
    }

    public void Add(ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Sensors.Sensors(zipCode, loginName)values(@zipCode,@loginName)",
        new { zipCode, loginName });
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
          "select id from Sensors where zipCode = @zipCode and loginName = @loginName",
          new { zipCode, loginName })
        .Any();
    }
  }
}
