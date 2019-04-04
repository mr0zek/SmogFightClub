using FluentMigrator;

namespace SFC.Processes.Features.UserRegistration.DbMigrations
{
  [Migration(201903310852)]
  public class AddSagasTable : Migration
  {
    public override void Up()
    {
      Create.Schema("Processes");
      Create.Table("Sagas")
        .InSchema("Processes")
        .WithColumn("Id").AsString().NotNullable()
        .WithColumn("Data").AsString(int.MaxValue).NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Sagas");
    }
  }
}
