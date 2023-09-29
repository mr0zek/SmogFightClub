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
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using SFC.Infrastructure.Interfaces;
using ArchUnitNET.xUnit;
using Autofac;
using FluentMigrator;

namespace SFC.Tests.Arch
{
  public class ArchitectureTests
  {
    private static readonly Architecture Architecture =
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
        Classes().That()
          .AreAssignableTo(typeof(Migration)).And()
          .AreAssignableTo(typeof(Exception)).And()
          .DoNotImplementInterface(typeof(IRequest<>)).And()
          .DoNotImplementInterface(typeof(IResponse)).And()
          .DoNotImplementInterface(typeof(ICommand)).And()
          .DoNotImplementInterface(typeof(IEvent)).And()
          .AreAssignableTo(typeof(Module))
          .Should().NotBePublic();

      

      allowedPublicTypesInModules.Check(Architecture);
    }
  }
}
