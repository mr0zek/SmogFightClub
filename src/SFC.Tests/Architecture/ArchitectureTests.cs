using System;
using Xunit;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Notifications;
using SFC.Sensors;
using SFC.Infrastructure.Interfaces;
using ArchUnitNET.xUnit;
using Autofac;
using FluentMigrator;
using System.Linq;
using System.Text.RegularExpressions;

namespace SFC.Tests.Architecture
{
  [Collection("Sequential")]
  public class ArchitectureTests
  {
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
    new ArchLoader().LoadAssemblies(
      typeof(AutofacAccountsModule).Assembly,
      typeof(AutofacAlertsModule).Assembly,
      typeof(AutofacNotificationsModule).Assembly,
      typeof(AutofacAlertsModule).Assembly,
      typeof(AutofacSensorsModule).Assembly).Build();

    [Fact]
    public void CheckPublicTypesInModules()
    {
      IArchRule allowedPublicTypesInModules =
        Types().That()
          .AreNotAssignableTo(typeof(IMigration)).And()
          .AreNotAssignableTo(typeof(Exception)).And()
          .DoNotImplementInterface(typeof(IRequest<>)).And()
          .DoNotImplementInterface(typeof(IResponse)).And()
          .DoNotImplementInterface(typeof(ICommand)).And()
          .DoNotImplementInterface(typeof(IEvent)).And()
          .AreNotAssignableTo(typeof(Module))
          .Should().NotBePublic();

      allowedPublicTypesInModules.Check(Architecture);
    }

    [Fact]
    public void CheckFeatureAutonomy()
    {
      var obj = Types().That().ResideInNamespace(".*Feature.*", true).GetObjects(Architecture);
      var namespaces = obj.Select(f =>
      {
        var m = Regex.Match(f.Namespace.ToString(), "(?<ns>.*Features[\\.].+[^\\.])[.].*");
        return m.Groups["ns"].Value;
      }).Where(f=>f!="").Distinct();


      foreach (var ns in namespaces)
      {
        foreach (var ns2 in namespaces)
        {
          if (ns != ns2)
          {
            Types().That().ResideInNamespace($"{ns}.*", true)
              .Should()
              .NotDependOnAny(
                Types()
                .That()
                .ResideInNamespace($"{ns2}.*", true)
                .And()
                .DoNotImplementInterface(typeof(ICommand))
                .And()
                .DoNotImplementInterface(typeof(IEvent))
                .And()
                .DoNotImplementInterface(typeof(IResponse))
                .And()
                .DoNotImplementInterface(typeof(IRequest<>))
                )

              .Check(Architecture);
          }
        }
      }
    }
  }
}
