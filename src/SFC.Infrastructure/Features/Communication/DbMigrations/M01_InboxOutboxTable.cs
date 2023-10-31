using FluentMigrator;

namespace SFC.Alerts.DbMigrations
{
  [Migration(20230331041)]
  public class M01_InboxOutboxTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Table("Inbox")
        .InSchema("dbo")
        .WithColumn("Id").AsInt32().PrimaryKey().Identity()
        .WithColumn("ModuleName").AsString().NotNullable()
        .WithColumn("LastProcessedId").AsString().NotNullable();

      Create.Table("Outbox")
        .InSchema("dbo")
        .WithColumn("Id").AsInt32().PrimaryKey().Identity()
        .WithColumn("Data").AsString().NotNullable()
        .WithColumn("Type").AsString().NotNullable();
    }    
  }
}
