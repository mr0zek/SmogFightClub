using FluentMigrator;

namespace SFC.AdminApi.Features.SearchableDashboard.DbMigrations
{
  [Migration(201904042139)]
  public class M01_AddSearchableDashboardTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      Create.Schema("SearchableDashboard");
      Create.Table("SearchableDashboard")
        .InSchema("SearchableDashboard")
        .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString(int.MaxValue).NotNullable()
        .WithColumn("alertsCount").AsInt16().NotNullable();
    }

  }
}
