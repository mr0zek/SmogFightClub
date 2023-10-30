using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

    public async Task Handle(AccountCreatedEvent @event, CancellationToken cancellationToken)
    {
      await _searchableDashboardPerspective.Add(new SearchableDashboardEntry
      {
        LoginName = @event.LoginName
      });
    }

    public async Task Handle(AlertCreatedEvent @event, CancellationToken cancellationToken)
    {
      var result = await _searchableDashboardPerspective.Get(@event.LoginName);
      result.AlertsCount++;
      await _searchableDashboardPerspective.Update(result);
    }
  }
}
