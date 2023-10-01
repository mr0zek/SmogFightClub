using Dapper;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Tests.DbMigrations
{
  public class InitializeDb
  {
    public static void Init(string connectionString)
    {
      new SqlConnection(connectionString).Execute("delete from Alerts.Alerts");
    }
  }
}

