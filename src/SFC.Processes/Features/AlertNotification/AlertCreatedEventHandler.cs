using SFC.Alerts.Contract.Event;
using SFC.Infrastructure;
using SFC.Notifications.Contract;

namespace SFC.Processes.Features.AlertNotification
{
  public class AlertCreatedEventHandler : IEventHandler<AlertRegisteredEvent>
  {
    private readonly ICommandBus _commandBus;

    public AlertCreatedEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public void Handle(AlertRegisteredEvent @event)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        LoginName = @event.LoginName
      });
    }
  }
}
