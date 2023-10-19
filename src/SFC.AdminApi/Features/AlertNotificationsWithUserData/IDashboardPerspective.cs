namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public interface IDashboardPerspective
  {
    DashboardResponse Search(AlertNotificationsWithUserRequest query);
  }
}