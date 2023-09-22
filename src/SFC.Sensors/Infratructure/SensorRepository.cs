using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.SensorQuery;
using SFC.SharedKernel;

namespace SFC.Sensors.Infratructure
{
  internal class SensorRepository : ISensorsPerspective, ISensorRepository
  {
    private readonly IDbConnection _connection;

    public SensorRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public SensorsReadModel GetAll(LoginName loginName)
    {
      return new SensorsReadModel(_connection.Query<SensorReadModel>(
        "select id, zipCode from Sensors.Sensors where loginName = @loginName", new { loginName = loginName.ToString() }));
    }

    public SensorReadModel Get(Guid sensorId, LoginName loginName)
    {
      return _connection.QueryFirst<SensorReadModel>("select id, zipCode from Sensors.Sensors where loginName = @loginName and id = @id", new { id = sensorId, loginName = loginName.ToString() });
    }

    public void Add(Guid sensorId, ZipCode zipCode, LoginName loginName)
    {
      _connection.Execute("insert into Sensors.Sensors(id, ZipCode, LoginName)values(@id, @zipCode, @loginName)",
        new { Id = sensorId, zipCode = zipCode.ToString(), loginName = loginName.ToString() });
    }

    public bool Exists(ZipCode zipCode, LoginName loginName)
    {
      return _connection.Query(
          "select id from Sensors where zipCode = @zipCode and loginName = @loginName",
          new { zipCode = zipCode.ToString(), loginName = loginName.ToString() })
        .Any();
    }

    public bool Exits(Guid sensorId)
    {
      return _connection.QueryFirst<int>("select count(*) from Sensors.Sensors where id = @id", new { Id = sensorId }) != 0;
    }

  }
}
