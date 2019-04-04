namespace SFC.AdminApi.Features.SearchableDashboard
{
  public class SearchableDashboardQueryModel
  {
    public int Take { get; set; }
    public int Skip { get; set; }
    public int AlertsCountGreaterThan { get; set; }
  }
}