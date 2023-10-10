using FluentMigrator;

namespace SFC.Accounts.Infrastructure.DbMigrations
{
  [Migration(202309292333)]
  public class M02_AddPasswordHasColumn : ForwardOnlyMigration
  {
    public override void Up()
    {
      Alter.Table("Accounts")
        .InSchema("Accounts")
        .AddColumn("PasswordHash").AsString(int.MaxValue).NotNullable();
    }
  }
}
