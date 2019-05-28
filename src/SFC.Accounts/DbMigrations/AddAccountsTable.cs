using FluentMigrator;

namespace SFC.Accounts.DbMigrations
{
  [Migration(201903312139)]
  public class AddAccountsTable : Migration
  {
    public override void Up()
    {
      Create.Schema("Accounts");
      Create.Table("Accounts")
        .InSchema("Accounts")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Accounts");
    }
  }
}
