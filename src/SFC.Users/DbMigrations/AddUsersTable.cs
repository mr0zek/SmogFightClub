using FluentMigrator;

namespace SFC.Users.DbMigrations
{
  [Migration(201903312139)]
  public class AddUsersTable : Migration
  {
    public override void Up()
    {
      Create.Schema("Users");
      Create.Table("Users")
        .InSchema("Users")
        .WithColumn("Id").AsString().NotNullable()
        .WithColumn("LoginName").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Users");
    }
  }
}
