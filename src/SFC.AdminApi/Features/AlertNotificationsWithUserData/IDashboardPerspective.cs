namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public interface IDashboardPerspective
  {
    DashboardResult Search(AlertNotificationsWithUserDataModel query);
  }
}