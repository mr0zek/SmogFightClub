using System;

namespace SFC.Notifications.Features.SendNotification
{
  internal interface IDateTimeProvider
  {
    DateTime Now();
  }
}