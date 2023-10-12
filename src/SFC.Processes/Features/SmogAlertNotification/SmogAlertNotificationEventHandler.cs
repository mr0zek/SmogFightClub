﻿using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.Processes.Features.SmogAlertNotification
{
  class SmogAlertEventHandler : IEventHandler<SmogAlertEvent>
  {
    private readonly ICommandBus _commandBus;

    public SmogAlertEventHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public void Handle(SmogAlertEvent @event)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        Title = "Smog alert",
        Body = $"Smog appears in your area, zip code: {@event.ZipCode}",
        LoginName = @event.LoginName,
        NotificationType = "SmogAlert"
      });
    }
  }
}
