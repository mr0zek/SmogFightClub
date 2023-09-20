using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterMeasurement.Command;

namespace SFC.SensorApi.Features.RecordMeasurement
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
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

    [HttpPost]
    public IActionResult Post([FromBody]PostMeasurementModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterMeasurementCommand()
      {
        Id = id,
        Date = _dateTimeProvider.Now(),
        Elements = model.Values.ToDictionary(f=>(ElementName)f.Key, f=>f.Value)
      });

      return Accepted($"api/measurements/{id}");
    }
  }
}