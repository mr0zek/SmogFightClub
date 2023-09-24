namespace SFC.AdminApi.Features.Dashboard
{
    public interface IDashboardPerspective
  {
    DashboardResult Search(DashboardQueryModel query);
  }
}