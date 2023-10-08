using FluentMigrator;

namespace SFC.Accounts.Infrastructure.DbMigrations
{
  [Migration(202310081718)]
  public class M03_AddAdminAccount : ForwardOnlyMigration
  {
    public override void Up()
    {
      Insert.IntoTable("Accounts")
        .InSchema("Accounts")
        .Row(new { LoginName = "admin", PasswordHash = @"_M?;Z?e?\u001d?'?????" });
    }
  }
}
