using FluentMigrator;

namespace SFC.Sensors.Features.RegisterMeasurement.Command
{
  [Migration(201904040951)]
  public class CreateMeasurementsTables : Migration
  {
    public override void Up()
    {
      Create.Table("Measurements")
        .InSchema("Sensors")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("SensorId").AsInt64().ForeignKey("FK_Measurements_Sensors", "Sensors", "Sensors", "Id")
        .WithColumn("ElementName").AsString().NotNullable()
        .WithColumn("ElementValue").AsDecimal().NotNullable();
    }

    public override void Down()
    {      
      Delete.Table("Sensors").InSchema("Sensors");
    }
  }
}