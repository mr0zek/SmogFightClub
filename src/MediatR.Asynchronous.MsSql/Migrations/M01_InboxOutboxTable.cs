using FluentMigrator;

namespace MediatR.Asynchronous.MsSql.Migrations
{
  [Migration(20230331041)]
  public class M01_InboxOutboxTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Table("Inbox")
        .InSchema("dbo")
        .WithColumn("Id").AsInt32().PrimaryKey()
        .WithColumn("Date").AsDateTime2().NotNullable()
        .WithColumn("ModuleName").AsString().NotNullable().PrimaryKey();

      Create.Table("Outbox")
        .InSchema("dbo")
        .WithColumn("Id").AsInt32().PrimaryKey().Identity()
        .WithColumn("Data").AsString().NotNullable()
        .WithColumn("Date").AsDateTime2().NotNullable()
        .WithColumn("MethodType").AsInt16().WithDefaultValue(2)
        .WithColumn("Type").AsString().NotNullable();
    }
  }
}
