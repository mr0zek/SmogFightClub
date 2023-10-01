namespace SFC.AdminApi.Features.SearchableDashboard
{
  public class SearchableDashboardQueryModel
  {
    public int Take { get; set; }
    public int Skip { get; set; }
    public int AlertsMin { get; set; }
    public int AlertsMax { get; set; }
  }
}