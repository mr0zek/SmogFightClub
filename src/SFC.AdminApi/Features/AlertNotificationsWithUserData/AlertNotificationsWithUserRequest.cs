using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public class AlertNotificationsWithUserRequest : IRequest<DashboardResponse>
  {
    public int Take { get; set; }
    public int Skip { get; set; }
  }
}