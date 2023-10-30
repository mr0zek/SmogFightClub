using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Processes.Features.AlertConditionCreatedNotification
{
    class AlertNotificationEventHandler : IEventHandler<AlertCreatedEvent>
  {
    private readonly ICommandBus _commandBus;

    public AlertNotificationEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public async Task Handle(AlertCreatedEvent @event, CancellationToken cancellationToken)
    {
      await _commandBus.Send(new SendNotificationCommand()
      {
        Title = "Smog alert created",
        Body = $"Smog alert has been succesfuly created, zip code: {@event.ZipCode}",
        LoginName = @event.LoginName,
        NotificationType = "AlertRegistered"
      });
    }
  }
}
