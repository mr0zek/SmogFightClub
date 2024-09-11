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
using FluentMigrator;
using System.Linq;
using System.Text.RegularExpressions;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SFC.UserApi;
using SFC.AdminApi;
using SFC.SensorApi;
using ArchUnitNET.Fluent.Predicates;
using System.Collections.Generic;
using ArchUnitNET.Domain.Extensions;
using SFC.Infrastructure.Interfaces.Modules;

namespace SFC.Tests.Architecture
{


  public class ArchitectureTests
  {
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
    new ArchLoader().LoadAssemblies(
      typeof(ICommand).Assembly,
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
          .ResideInAssembly("SFC.*", true).And()
          .DoNotResideInAssembly(typeof(ICommand).Assembly).And()
          .AreNotAssignableTo(typeof(IMigration)).And()
          .AreNotAssignableTo(typeof(Exception)).And()
          .AreNotAssignableTo(typeof(IModule)).And()
          .DoNotImplementInterface(typeof(IRequest<>)).And()
          .DoNotImplementInterface(typeof(IActionFilter)).And()
          .DoNotImplementInterface(typeof(IResponse)).And()
          .DoNotImplementInterface(typeof(ICommand)).And()
          .DoNotImplementInterface(typeof(IEvent))
          .Should().NotBePublic();

      allowedPublicTypesInModules.Check(Architecture);
    }    
   
    [Fact]
    public void CheckFeatureAutonomy()
    {
      var obj = Types().That()
        .ResideInAssembly("SFC.*",true).And()
        .ResideInNamespace(".*Feature.*", true).GetObjects(Architecture);
      var namespaces = obj.Select(f =>
      {
        var m = Regex.Match(f.Namespace.ToString(), "(?<ns>.*Features[.][^.]+)");
        return m.Groups["ns"].Value;
      }).Where(f => f != "").Distinct();


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
