using SFC.Infrastructure.Interfaces.Documentation;
using System;

namespace SFC.Infrastructure.Interfaces.TimeDependency
{  
  public interface IDateTimeProvider
  {
    [ExitPointTo("Time", CallType.Query)]
    DateTime Now();
  }
}