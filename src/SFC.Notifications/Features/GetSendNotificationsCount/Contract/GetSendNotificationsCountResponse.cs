using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetSendNotificationsCount.Contract
{
  public class GetSendNotificationsCountResponse
  {
    public LoginName LoginName { get; set; }
    public int Count { get; set; }
  }
}
