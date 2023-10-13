using System;
using System.Collections.Generic;
using System.Text;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.AdminApi.Features.SearchableDashboard
{
    class SearchableDashboardHandler : IEventHandler<AccountCreatedEvent>, IEventHandler<AlertCreatedEvent>
  {
    private readonly IWriteDashboardPerspective _searchableDashboardPerspective;

    public SearchableDashboardHandler(IWriteDashboardPerspective searchableDashboardPerspective)
    {
      _searchableDashboardPerspective = searchableDashboardPerspective;
    }

    public void Handle(AccountCreatedEvent @event)
    {
      _searchableDashboardPerspective.Add(new SearchableDashboardEntry
      {
        LoginName = @event.LoginName
      });
    }

    public void Handle(AlertCreatedEvent @event)
    {
      var result = _searchableDashboardPerspective.Get(@event.LoginName);
      result.AlertsCount++;
      _searchableDashboardPerspective.Update(result);
    }
  }
}
