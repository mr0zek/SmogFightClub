using FluentMigrator;

namespace SFC.AdminApi.Features.SearchableDashboard.DbMigrations
{
  [Migration(201904042139)]
  public class AddSearchableDashboardTable : Migration
  {
    public override void Up()
    {
      Create.Schema("SearchableDashboard");
      Create.Table("SearchableDashboard")
        .InSchema("SearchableDashboard")
        .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString(int.MaxValue).NotNullable()
        .WithColumn("alertCount").AsInt16().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("SearchableDashboard");
    }
  }
}
