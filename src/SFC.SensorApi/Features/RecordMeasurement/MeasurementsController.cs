using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Contract;

namespace SFC.SensorApi.Features.RecordMeasurement
{
    [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}")]
  [ApiController]
  public class MeasurementsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IDateTimeProvider _dateTimeProvider;

    public MeasurementsController(ICommandBus commandBus, IDateTimeProvider dateTimeProvider)
    {
      _commandBus = commandBus;
      _dateTimeProvider = dateTimeProvider;
    }

    [HttpPost("sensors/{sensorId}/measurements")]
    public IActionResult Post([FromRoute] Guid sensorId, [FromBody] PostMeasurementModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterMeasurementCommand()
      {
        Id = id,
        SensorId = sensorId,
        Date = _dateTimeProvider.Now(),
        Elements = model.Values.ToDictionary(f => (ElementName)f.Key, f => f.Value)
      });

      return Ok();
    }
  }
}