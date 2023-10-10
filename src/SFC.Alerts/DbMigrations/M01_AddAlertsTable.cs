using FluentMigrator;

namespace SFC.Alerts.DbMigrations
{
  [Migration(20190331041)]
  public class M01_AddAlertsTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Schema("Alerts");
      Create.Table("Alerts")
        .InSchema("Alerts")
        .WithColumn("Id").AsGuid().PrimaryKey()
        .WithColumn("LoginName").AsString().NotNullable()
        .WithColumn("ZipCode").AsString().NotNullable();
    }    
  }
}
