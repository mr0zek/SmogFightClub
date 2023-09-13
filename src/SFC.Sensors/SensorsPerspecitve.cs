using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Sensors.Features.RegisterMeasurement.Query;
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
        "select id, zipCode from Sensors.Sensors where loginName = @loginName", new { loginName = loginName.ToString() }));
    }

    public SensorReadModel Get(string id, LoginName loginName)
    {
      return _connection.QueryFirst<SensorReadModel>("select id,zipCode from Sensors.Sensors where loginName = @loginName nad id = @id", new { id, loginName = loginName.ToString() });
    }

    public void Add(ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Sensors.Sensors(zipCode, loginName)values(@zipCode,@loginName)",
        new { zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
          "select id from Sensors where zipCode = @zipCode and loginName = @loginName",
          new { zipCode = zipCode.ToString(), loginName = loginName.ToString() })
        .Any();
    }
  }
}
