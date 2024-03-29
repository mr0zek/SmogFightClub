﻿using FluentMigrator;

namespace SFC.Accounts.Infrastructure.DbMigrations
{
  [Migration(201903312139)]
    public class M01_AddAccountsTable : ForwardOnlyMigration
  {
        public override void Up()
        {
            Create.Schema("Accounts");
            Create.Table("Accounts")
              .InSchema("Accounts")
              .WithColumn("Id").AsInt64().PrimaryKey().Identity()
              .WithColumn("LoginName").AsString(int.MaxValue).NotNullable();
        }

    }
}
