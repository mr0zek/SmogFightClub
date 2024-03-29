﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Infrastructure.Interfaces.TimeDependency;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using static SFC.Sensors.Features.RegisterMeasurement.Contract.RegisterMeasurementCommand;

namespace SFC.SensorApi.Features.RegisterMeasurement
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

    [EntryPointFor("Sensor", CallerType.ExternalSystem, CallType.Command)]
    [AllowAnonymous]
    [HttpPost("sensors/{sensorId}/measurements")]
    public async Task<IActionResult> Post([FromRoute] Guid sensorId, [FromBody] PostMeasurementModel model)
    {
      Guid id = Guid.NewGuid();

      await _commandBus.Send(new RegisterMeasurementCommand()
      {
        SensorId = sensorId,
        Date = _dateTimeProvider.Now(),
        Elements = model.Values.ToDictionary(f => f.Key, f => f.Value)
      });

      return Ok();
    }
  }
}