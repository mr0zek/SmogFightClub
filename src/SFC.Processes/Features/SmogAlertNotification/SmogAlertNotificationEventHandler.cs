using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Processes.Features.SmogAlertNotification
{
    class SmogAlertEventHandler : IEventHandler<SmogAlertEvent>
  {
    private readonly ICommandBus _commandBus;

    public SmogAlertEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public async Task Handle(SmogAlertEvent @event, CancellationToken token)
    {
      await _commandBus.Send(new SendNotificationCommand()
      {
        Title = "Smog alert",
        Body = $"Smog appears in your area, zip code: {@event.ZipCode}",
        LoginName = @event.LoginName,
        NotificationType = "SmogAlert"
      });
    }
  }
}
