using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification.Contract
{
  public class SendNotificationCommand : ICommand
  {
    public LoginName LoginName { get; set; }
    public string Body { get; set; }
    public string Title { get; set; }
    public string NotificationType { get; set; }

    public SendNotificationCommand(LoginName loginName, string body, string title, string notificationType)
    {
      LoginName = loginName;
      Body = body;
      Title = title;
      NotificationType = notificationType;
    }

  }
}