using System.Threading.Tasks;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  public interface ISearchabelDashboardPerspective
  {
    Task<SearchableDashboardResult> Search(SearchableDashboardQueryModel query);
  }
}