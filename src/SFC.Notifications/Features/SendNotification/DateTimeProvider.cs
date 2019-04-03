using System;

namespace SFC.Notifications.Features.SendNotification
{
  class DateTimeProvider : IDateTimeProvider
  {
    public DateTime Now()
    {
      return DateTime.Now;
    }
  }
}