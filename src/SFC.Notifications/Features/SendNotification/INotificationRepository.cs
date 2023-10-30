using System;
using System.Threading.Tasks;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  internal interface INotificationRepository 
  {
    Task Add(Email email, string title, string body, DateTime date, LoginName loginName, string notificationType);
  }
}