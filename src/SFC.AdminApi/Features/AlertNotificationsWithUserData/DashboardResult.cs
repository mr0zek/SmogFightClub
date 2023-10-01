using System.Collections.Generic;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public class DashboardResult
  {
    public IEnumerable<DashboardEntry> Results { get; }

    public DashboardResult(IEnumerable<DashboardEntry> results)
    {
      Results = results;
    }
  }
}