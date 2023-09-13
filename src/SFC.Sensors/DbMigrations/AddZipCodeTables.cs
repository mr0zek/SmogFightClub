using FluentMigrator;

namespace SFC.Sensors.DbMigrations
{
  [Migration(202309131437)]
  public class AddZipCodeTables : Migration
  {
    public override void Up()
    {     
      Alter.Table("Sensors")
        .InSchema("Sensors")
        .AddColumn("ZipCode").AsString().NotNullable();      
    }

    public override void Down()
    {
      Delete.Column("ZipCode").FromTable("Sensors").InSchema("Sensors");
    }
  }
}
