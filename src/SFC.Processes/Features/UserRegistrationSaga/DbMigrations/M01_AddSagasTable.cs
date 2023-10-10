using FluentMigrator;

namespace SFC.Processes.Features.UserRegistrationSaga.DbMigrations
{
  [Migration(201903310852)]
  public class M01_AddSagasTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Schema("Processes");
      Create.Table("Sagas")
        .InSchema("Processes")
        .WithColumn("Id").AsString().NotNullable()
        .WithColumn("Data").AsString(int.MaxValue).NotNullable();
    }

  }
}
