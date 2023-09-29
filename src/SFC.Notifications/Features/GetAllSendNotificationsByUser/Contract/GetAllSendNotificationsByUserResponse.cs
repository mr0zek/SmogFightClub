using SFC.SharedKernel;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract
{
  public class GetAllSendNotificationsByUserResponse
  {
    public LoginName LoginName { get; set; }
    public int Count { get; set; }
  }
}