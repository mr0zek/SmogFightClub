using FluentMigrator;

namespace SFC.Accounts.Infrastructure.DbMigrations
{
  [Migration(202309292333)]
  public class M02_AddPasswordHasColumn : Migration
  {
    public override void Up()
    {
      Alter.Table("Accounts")
        .InSchema("Accounts")
        .AddColumn("PasswordHash").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
      Delete.Column("PasswordHash").FromTable("Accounts").InSchema("Accounts");
    }
  }
}
