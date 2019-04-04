using SFC.Alerts.Features.RegisterAlert.Contract;
using SFC.Infrastructure;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.Processes.Features.AlertCreatedNotification
{
  public class AlertNotificationEventHandler : IEventHandler<AlertRegisteredEvent>
  {
    private readonly ICommandBus _commandBus;

    public AlertNotificationEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public void Handle(AlertRegisteredEvent @event)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        Title = "Smog alert created",
        Body = $"Smog alert ha been succesfuly created, zip code: {@event.ZipCode}",
        LoginName = @event.LoginName
      });
    }
  }
}
