using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetAllSendNotificationsCount.Contract
{
  public class GetAllSendNotificationsCountResponse
  {
    public LoginName LoginName { get; set; }
    public int Count { get; set; }
  }
}