using System;
using System.Collections.Generic;
using System.Text;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  class SearchableDashboardHandler : IEventHandler<AccountCreatedEvent>, IEventHandler<NotificationSentEvent>
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

    public void Handle(NotificationSentEvent @event)
    {
      if (@event.NotificationType == "SmogAlert")
      {
        var result = _searchableDashboardPerspective.Get(@event.LoginName);
        result.AlertsSentCount++;
        _searchableDashboardPerspective.Update(result);
      }
    }
  }
}
