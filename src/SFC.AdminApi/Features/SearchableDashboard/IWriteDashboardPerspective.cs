using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  internal interface IWriteDashboardPerspective 
  {
    Task Add(SearchableDashboardEntry searchableDashboardEntry);
    Task<SearchableDashboardEntry> Get(LoginName eventLoginName);
    Task Update(SearchableDashboardEntry searchableDashboardEntry);
  }
}