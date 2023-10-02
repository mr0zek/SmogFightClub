using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Alerts.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
    internal class VerifySmogExceedenceHandler : IEventHandler<AcceptableLevelExceededEvent>
  {
    private readonly IAlertRepository _alertRepository;
    private readonly IEventBus _eventBus;

    public VerifySmogExceedenceHandler(IAlertRepository alertRepository, IEventBus eventBus)
    {
      _alertRepository = alertRepository;
      _eventBus = eventBus;
    }

    public void Handle(AcceptableLevelExceededEvent @event)
    {
      IEnumerable<Alert> alert = _alertRepository.GetByZipCode(@event.ZipCode);
      foreach (var item in alert)
      {
        _eventBus.Publish(new SmogAlertEvent(item.LoginName, @event.ZipCode));
      }
    }
  }
}
