using FluentMigrator;

namespace SFC.Sensors.DbMigrations
{
  [Migration(201904040952)]
  public class CreateTables : Migration
  {
    public override void Up()
    {
      Create.Schema("Sensors");
      Create.Table("Sensors")
        .InSchema("Sensors")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString().NotNullable();

      Create.Table("Measurements")
        .InSchema("Sensors")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("SensorId").AsInt64().ForeignKey("FK_Measurements_Sensors", "Sensors", "Sensors", "Id")
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
