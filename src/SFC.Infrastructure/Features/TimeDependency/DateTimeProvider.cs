using SFC.Infrastructure.Interfaces;
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