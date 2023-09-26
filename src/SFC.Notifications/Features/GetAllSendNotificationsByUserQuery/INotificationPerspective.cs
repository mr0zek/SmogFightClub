using System;
using System.Collections.Generic;
using System.Text;
using SFC.Notifications.Features.GetAllSendNotificationsByUserQuery.Contract;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUserQuery
{
    internal interface INotificationPerspective
  {
     IEnumerable<NotificationsCountResult> GetSendNotificationsCount(
      string notificationType,
      params LoginName[] loginNames);

  }
}
