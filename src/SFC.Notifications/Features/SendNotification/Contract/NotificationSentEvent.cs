using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification.Contract
{
  public class NotificationSentEvent : IEvent
  {
    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public string NotificationType { get; set; }
  }
}