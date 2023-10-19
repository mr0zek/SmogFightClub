using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract;

namespace SFC.AdminApi.Features.SendNotificationsByUser
{
  public class SendNotificationsByUserModel : IRequest<GetAllSendNotificationsByUserResponse>
  {
    public int Take { get; set; }
    public int Skip { get; set; }
  }
}