using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  public class NotificationSentEvent
  {
    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public string NotificationType { get; set; }
  }
}