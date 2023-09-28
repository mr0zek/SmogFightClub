using FluentMigrator;

namespace SFC.Notifications.DbMigrations
{
  [Migration(201903311041)]
  public class M01_InitTables : Migration
  {
    public override void Up()
    {
      Create.Schema("Notifications");

      Create.Table("Notifications")
        .InSchema("Notifications")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString().NotNullable()
        .WithColumn("Body").AsString().NotNullable()
        .WithColumn("Date").AsDateTime().NotNullable()
        .WithColumn("Email").AsString().NotNullable()
        .WithColumn("NotificationType").AsString().NotNullable()
        .WithColumn("Title").AsString().NotNullable();

      Create.Table("Emails")
        .InSchema("Notifications")
        .WithColumn("LoginName").AsString().Unique()
        .WithColumn("Email").AsString().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Notifications").InSchema("Notifications");
      Delete.Table("Emails").InSchema("Notifications");
    }
  }
}
