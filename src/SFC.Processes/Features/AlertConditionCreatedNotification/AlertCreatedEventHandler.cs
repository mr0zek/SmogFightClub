using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.Processes.Features.AlertConditionCreatedNotification
{
  public class AlertConditionNotificationEventHandler : IEventHandler<AlertCreatedEvent>
  {
    private readonly ICommandBus _commandBus;

    public AlertConditionNotificationEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public void Handle(AlertCreatedEvent @event)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        Title = "Smog alert created",
        Body = $"Smog alert has been succesfuly created, zip code: {@event.ZipCode}",
        LoginName = @event.LoginName,
        NotificationType = "AlertConditionRegistered"
      });
    }
  }
}
