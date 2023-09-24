using System;
using System.Collections.Generic;
using System.Text;
using SFC.Notifications.Infrastructure;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.NotificationQuery
{
  public interface INotificationPerspective
  {
    IEnumerable<NotificationsCountResult> GetAllSendNotificationsByUser(int top, int take);
    IEnumerable<NotificationsCountResult> GetSendNotificationsCount(
      string notificationType,
      params LoginName[] loginNames);

  }
}
