using System;

namespace SFC.Infrastructure.Interfaces.TimeDependency
{  
  public interface IDateTimeProvider
  {
    DateTime Now();
  }
}