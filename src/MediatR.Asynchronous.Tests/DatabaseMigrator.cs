using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Asynchronous.Tests
{
  class DatabaseMigrator
  {
    public static void Run(string connectionString)
    {
      var assemblies = Directory.GetFiles(
        AppDomain.CurrentDomain.BaseDirectory, "*.*")
        .Where(f => Path.GetExtension(f).ToLower() == ".dll")
        .Where(f => Path.GetFileName(f).Contains("MediatR.Asynchronous.MsSql"))
        .Select(f => Assembly.LoadFile(f)).ToArray();

      var serviceCollection = new ServiceCollection();
      var sp = serviceCollection.AddFluentMigratorCore()
        .ConfigureRunner(rb =>
        {
          rb
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(assemblies).For.Migrations();
        })
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);

      sp.GetRequiredService<IMigrationRunner>().MigrateUp();
    }
  }
}