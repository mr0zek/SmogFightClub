using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Sensors.Contract.Command;
using SFC.Sensors.Contract.Query;
using SFC.SharedKernel;

namespace SFC.Api.Features.Sensors
{
  [Route("api/[controller]")]
  [ApiController]
  public class SensorsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly ISensorsPerspective _sensorsPerspective;

    public SensorsController(ICommandBus commandBus, ISensorsPerspective sensorsPerspective)
    {
      _commandBus = commandBus;
      _sensorsPerspective = sensorsPerspective;
    }

    [HttpPost]
    public IActionResult Post(PostSensorModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterSensorCommand()
      {
        Id = id,
        LoginName = GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Accepted($"api/sensors/{id}");
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_sensorsPerspective.GetAll(GetLoginName()));
    }

    private LoginName GetLoginName()
    {
      return User.Identity.Name;
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Json(_sensorsPerspective.Get(id, GetLoginName()));
    }
  }
}