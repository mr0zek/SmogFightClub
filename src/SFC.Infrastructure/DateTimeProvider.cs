using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Infrastructure
{
  class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now()
    {
      return DateTime.Now;
    }
  }
}