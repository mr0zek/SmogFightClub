using SFC.Infrastructure.Interfaces.TimeDependency;
using System;

namespace SFC.Infrastructure.Features.TimeDependency
{
  class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}