using SFC.SharedKernel;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public class DashboardEntry
  {
    public string LoginName { get; set; }
    public int AlertsSentCount { get; set; }
  }
}