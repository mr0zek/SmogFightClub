using System.Linq;
using SFC.Accounts.Features.AccountQuery;
using SFC.Notifications.Features.NotificationQuery;

namespace SFC.AdminApi.Features.Dashboard
{
    class DashboardPerspective : IDashboardPerspective
  {
    private readonly IAccountsPerspective _accountPerspective;
    private readonly INotificationPerspective _notificationPerspective;

    public DashboardPerspective(
      IAccountsPerspective accountPerspective, 
      INotificationPerspective notificationPerspective)
    {
      _accountPerspective = accountPerspective;
      _notificationPerspective = notificationPerspective;
    }

    public DashboardResult Search(DashboardQueryModel query)
    {
      var results = _accountPerspective.Search(new AccountQuery()
      {
        Skip = query.Top,
        Take = query.Take
      });

      var counts = _notificationPerspective.GetSendNotificationsCount("SmogAlert", results.Accounts.Select(f => f.LoginName).ToArray());

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