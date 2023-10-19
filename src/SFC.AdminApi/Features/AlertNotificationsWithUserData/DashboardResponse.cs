using SFC.Infrastructure.Interfaces.Communication;
using System.Collections.Generic;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  public class DashboardResponse : IResponse
  {
    public IEnumerable<DashboardEntry> Results { get; }

    public DashboardResponse(IEnumerable<DashboardEntry> results)
    {
      Results = results;
    }
  }
}