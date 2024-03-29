﻿using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
    internal class VerifySmogExceedenceHandler : IEventHandler<AcceptableLevelExceededEvent>
  {
    private readonly IAlertReadRepository _alertRepository;
    private readonly IEventBus _eventBus;

    public VerifySmogExceedenceHandler(IAlertReadRepository alertRepository, IEventBus eventBus)
    {
      _alertRepository = alertRepository;
      _eventBus = eventBus;
    }

    public async Task Handle(AcceptableLevelExceededEvent @event, CancellationToken cancellationToken)
    {
      IEnumerable<Alert> alert = await _alertRepository.GetByZipCode(@event.ZipCode);
      foreach (var item in alert)
      {
        await _eventBus.Publish(new SmogAlertEvent(item.LoginName, @event.ZipCode));
      }
    }
  }
}
