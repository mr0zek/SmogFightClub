using System;

namespace SFC.Infrastructure
{
  public interface IDateTimeProvider
  {
    DateTime Now();
  }
}