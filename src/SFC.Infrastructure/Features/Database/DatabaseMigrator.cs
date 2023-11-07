using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Database;

namespace SFC.Infrastructure.Features.Database
{
  class DatabaseMigrator : IDatabaseMigrator
  {
    private ConnectionString _connectionString;

    public DatabaseMigrator(ConnectionString connectionString)
    {
      _connectionString = connectionString;
    }
    public void Run()
    {
      var assemblies = Directory.GetFiles(
        AppDomain.CurrentDomain.BaseDirectory, "*.*")
        .Where(f => Path.GetExtension(f).ToLower() == ".dll")
        .Where(f => Path.GetFileName(f).Contains("SFC"))
        .Select(f => Assembly.LoadFile(f)).ToArray();

      var serviceCollection = new ServiceCollection();
      var sp = serviceCollection.AddFluentMigratorCore()
        .ConfigureRunner(rb =>
        {
          rb
            .AddSqlServer()
            .WithGlobalConnectionString(_connectionString.ToString())
            .ScanIn(assemblies).For.Migrations();
        })
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

      sp.GetRequiredService<IMigrationRunner>().MigrateUp();
    }
  }
}