﻿using FluentMigrator;

namespace SFC.Alerts.DbMigrations
{
  [Migration(201903310741)]
  public class AddAlertsTable : Migration
  {
    public override void Up()
    {
      Create.Schema("Alerts");
      Create.Table("Alerts")
        .InSchema("Alerts")
        .WithColumn("Id").AsInt64().PrimaryKey().Identity()
        .WithColumn("LoginName").AsString().NotNullable()
        .WithColumn("ZipCode").AsString().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Alerts");
    }
  }
}
