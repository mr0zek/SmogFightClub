using System.Diagnostics.Contracts;
using System.Linq;
using SFC.Accounts.Features.SearchAccount;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.GetSendNotificationsCount.Contract;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  class DashboardPerspective : IDashboardPerspective
  {
    private readonly IQuery _query;

    public DashboardPerspective(
      IQuery notificationPerspective)
    {
      _query = notificationPerspective;
    }

    public DashboardResult Search(AlertNotificationsWithUserDataModel query)
    {
      var results = _query.Query(new SearchAccountRequest()
      {
        Skip = query.Skip,
        Take = query.Take
      });

      var counts = _query.Query(new GetSendNotificationsCountRequest()
      {
        NotificationType = "SmogAlert",
        LoginNames = results.Accounts.Select(f => f.LoginName).ToArray()
      });

      var entries = results.Accounts.Select(f =>
      {
        int count = counts.Result.FirstOrDefault(c => c.LoginName == f.LoginName)?.Count ?? 0;
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