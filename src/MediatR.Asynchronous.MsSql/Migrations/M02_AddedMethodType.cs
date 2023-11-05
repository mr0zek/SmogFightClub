using FluentMigrator;

namespace MediatR.Asynchronous.MsSql.Migrations
{
  [Migration(20231031041)]
  public class M02_AddedMethodType : ForwardOnlyMigration
  {
    public override void Up()
    {
      Alter.Table("Outbox")
        .InSchema("dbo").AddColumn("MethodType").AsInt16().WithDefaultValue(2);
    }
  }
}
