using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.RegisterSensor.Contract;
using SFC.Sensors.Features.SensorQuery.Contract;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Sensors
{
    [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/user/[controller]")]
  [ApiController]
  public class SensorsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly ISensorsPerspective _sensorsPerspective;
    private readonly IIdentityProvider _identityProvider;

    public SensorsController(ICommandBus commandBus, ISensorsPerspective sensorsPerspective, IIdentityProvider identityProvider)
    {
      _commandBus = commandBus;
      _sensorsPerspective = sensorsPerspective;
      _identityProvider = identityProvider;
    }

    [HttpPost]
    public IActionResult Post([FromBody]PostSensorModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterSensorCommand()
      {
        Id = id,
        LoginName = _identityProvider.GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Accepted($"api/v1/sensors/{id}",id);
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_sensorsPerspective.GetAll(_identityProvider.GetLoginName()));
    }    

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Json(_sensorsPerspective.Get(new Guid(id), _identityProvider.GetLoginName()));
    }
  }
}