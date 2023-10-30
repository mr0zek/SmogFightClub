using System.Threading.Tasks;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public interface IDashboardPerspective
  {
    Task<DashboardResponse> Search(AlertNotificationsWithUserRequest query);
  }
}