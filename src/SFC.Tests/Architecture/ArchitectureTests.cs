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
using ArchUnitNET.xUnit;
using Autofac;
using FluentMigrator;
using System.Linq;
using System.Text.RegularExpressions;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.Tests.Architecture
{

    public class ArchitectureTests
  {
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
    new ArchLoader().LoadAssemblies(
      typeof(AccountsModule).Assembly,
      typeof(AlertsModule).Assembly,
      typeof(NotificationsModule).Assembly,
      typeof(AlertsModule).Assembly,
      typeof(SensorsModule).Assembly).Build();

    [Fact]
    public void CheckPublicTypesInModules()
    {
      IArchRule allowedPublicTypesInModules =
        Types().That()
          .AreNotAssignableTo(typeof(IMigration)).And()
          .AreNotAssignableTo(typeof(Exception)).And()
          .DoNotHaveAnyAttributes(typeof(ModuleDefinitionAttribute)).And()
          .DoNotImplementInterface(typeof(IRequest<>)).And()
          .DoNotImplementInterface(typeof(IResponse)).And()
          .DoNotImplementInterface(typeof(ICommand)).And()
          .DoNotImplementInterface(typeof(IEvent))
          .Should().NotBePublic();

      allowedPublicTypesInModules.Check(Architecture);
    }

    [Fact]
    public void EveryControllerHaveToHaveEntryPointForDeclared()
    {

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
