namespace SFC.AdminApi.Features.SearchableDashboard
{
  public interface ISearchabelDashboardPerspective
  {
    SearchableDashboardResult Search(SearchableDashboardQueryModel query);
  }
}