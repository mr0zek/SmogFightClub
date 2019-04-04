using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification.Contract
{
  public class SendNotificationCommand
  {
    public LoginName LoginName { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }
  }
}