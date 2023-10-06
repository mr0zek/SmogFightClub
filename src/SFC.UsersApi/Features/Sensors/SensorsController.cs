using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
using SFC.Sensors.Features.GetAllSensors;
using SFC.Sensors.Features.GetSensor;
using SFC.Sensors.Features.RegisterSensor.Contract;

namespace SFC.UserApi.Features.Sensors
{
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/user/[controller]")]
  [ApiController]
  public class SensorsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IQuery _query;
    private readonly IIdentityProvider _identityProvider;

    public SensorsController(ICommandBus commandBus, IIdentityProvider identityProvider, IQuery query)
    {
      _commandBus = commandBus;
      _identityProvider = identityProvider;
      _query = query;
    }

    [HttpPost]
    public IActionResult Post([FromBody]PostSensorModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterSensorCommand()
      {
        SensorId = id,
        LoginName = _identityProvider.GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Accepted($"api/v1/sensors/{id}",id);
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_query.Query(new GetAllSensorsRequest(_identityProvider.GetLoginName())));
    }    

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Json(_query.Query(new GetSensorRequest(new Guid(id), _identityProvider.GetLoginName())));
    }
  }
}