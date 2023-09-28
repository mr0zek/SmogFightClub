using FluentMigrator;

namespace SFC.Sensors.Infrastructure.DbMigrations
{
  [Migration(201904040952)]
  public class M01_CreateTables : Migration
  {
    public override void Up()
    {
      Create.Schema("Sensors");
      Create.Table("Sensors")
        .InSchema("Sensors")
        .WithColumn("Id").AsGuid().PrimaryKey()
        .WithColumn("ZipCode").AsString().NotNullable()
        .WithColumn("LoginName").AsString().NotNullable();

      Create.Table("Measurements")
        .InSchema("Sensors")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("SensorId").AsGuid().ForeignKey("FK_Measurements_Sensors", "Sensors", "Sensors", "Id")
        .WithColumn("Date").AsDateTime2().NotNullable()
        .WithColumn("ElementName").AsString().NotNullable()
        .WithColumn("ElementValue").AsDecimal().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Measurements").InSchema("Sensors");
      Delete.Table("Sensors").InSchema("Sensors");
    }
  }
}
