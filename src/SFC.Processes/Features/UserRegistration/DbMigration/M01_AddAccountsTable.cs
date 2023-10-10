using FluentMigrator;

namespace SFC.Processes.Features.UserRegistration.DbMigration
{
  [Migration(202310101252)]
  public class M01_AddAccountsTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Table("Accounts")
        .InSchema("Processes")
        .WithColumn("Id").AsGuid()
        .WithColumn("Email").AsString().NotNullable()
        .WithColumn("ZipCode").AsString().NotNullable()
        .WithColumn("PasswordHash").AsString().NotNullable()
        .WithColumn("LoginName").AsString(int.MaxValue).NotNullable();
    }

  }
}
