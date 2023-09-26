using System.Diagnostics.Contracts;
using System.Linq;
using SFC.Accounts.Features.AccountQuery;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.GetAllSendNotificationsCountQuery.Contract;
using SFC.Notifications.Features.GetSendNotificationsCountQuery.Contract;

namespace SFC.AdminApi.Features.Dashboard
{
  class DashboardPerspective : IDashboardPerspective
  {    
    private readonly IQueryBus _query;

    public DashboardPerspective(      
      IQueryBus notificationPerspective)
    {
      _query = notificationPerspective;
    }

    public DashboardResult Search(DashboardQueryModel query)
    {
      var results = _query.Query(new AccountQuery()
      {
        Skip = query.Top,
        Take = query.Take
      });

      var counts = _query.Query(new GetSendNotificationsCountRequest("SmogAlert", results.Accounts.Select(f => f.LoginName).ToArray()));

      var entries = results.Accounts.Select(f =>
      {
        int count = counts.FirstOrDefault(c => c.LoginName == f.LoginName)?.Count ?? 0;
        return new DashboardEntry()
        {
          LoginName = f.LoginName,
          AlertsSentCount = count
        };
      });

      return new DashboardResult(entries);
    }
  }
}