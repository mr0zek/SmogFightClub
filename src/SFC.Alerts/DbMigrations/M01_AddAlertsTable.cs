using FluentMigrator;

namespace SFC.Alerts.DbMigrations
{
  [Migration(20190331041)]
  public class M01_AddAlertsTable : Migration
  {
    public override void Up()
    {
      Create.Schema("Alerts");
      Create.Table("Alerts")
        .InSchema("Alerts")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString().NotNullable()
        .WithColumn("ZipCode").AsString().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Alerts").InSchema("Alerts");
    }
  }
}
