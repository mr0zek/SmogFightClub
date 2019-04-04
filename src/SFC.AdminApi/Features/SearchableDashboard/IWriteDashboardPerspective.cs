using SFC.SharedKernel;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  internal interface IWriteDashboardPerspective 
  {
    void Add(SearchableDashboardEntry searchableDashboardEntry);
    SearchableDashboardEntry Get(LoginName eventLoginName);
    void Update(SearchableDashboardEntry searchableDashboardEntry);
  }
}