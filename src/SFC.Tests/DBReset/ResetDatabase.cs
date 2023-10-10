using Dapper;
using FluentMigrator;
using SFC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Tests.DBReset
{  
  internal class ResetDatabase 
  {
    public static void Reset(string connectionString)
    {
      var connection = new SqlConnection(connectionString);

      connection.Execute(
        @"DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR

        SET @Cursor = CURSOR FAST_FORWARD FOR
        SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_SCHEMA + '].[' +  tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + '];'
        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
        LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME

        OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

        WHILE (@@FETCH_STATUS = 0)
        BEGIN
        Exec sp_executesql @Sql
        FETCH NEXT FROM @Cursor INTO @Sql
        END

        CLOSE @Cursor DEALLOCATE @Cursor

        EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
        EXEC sp_MSforeachtable 'SET QUOTED_IDENTIFIER ON; drop table ?;'

        SET @Cursor = CURSOR FAST_FORWARD FOR
        select 'drop schema '+s.name
        from sys.schemas s inner join sys.sysusers u  on u.uid = s.principal_id
        where u.name = 'dbo' and s.name <> 'dbo'

        OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

        WHILE (@@FETCH_STATUS = 0)
        BEGIN
        Exec sp_executesql @Sql
        FETCH NEXT FROM @Cursor INTO @Sql
        END

        CLOSE @Cursor DEALLOCATE @Cursor");      
    }
  }
}
