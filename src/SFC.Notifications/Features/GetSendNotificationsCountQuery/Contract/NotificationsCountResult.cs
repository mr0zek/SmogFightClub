using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetSendNotificationsCountQuery.Contract
{
  public class NotificationsCountResult
  {
    public LoginName LoginName { get; set; }
    public int Count { get; set; }
  }
}
