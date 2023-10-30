using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
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

    [EntryPointFor("User", CallerType.Human, CallType.Command)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]PostSensorModel model)
    {
      Guid id = Guid.NewGuid();

      await _commandBus.Send(new RegisterSensorCommand()
      {
        SensorId = id,
        LoginName = _identityProvider.GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Accepted($"api/v1/sensors/{id}",id);
    }

    [EntryPointFor("User", CallerType.Human, CallType.Query)]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Json(await _query.Send(new GetAllSensorsRequest(_identityProvider.GetLoginName())));
    }

    [EntryPointFor("User", CallerType.Human, CallType.Query)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
      return Json(await _query.Send(new GetSensorRequest(new Guid(id), _identityProvider.GetLoginName())));
    }
  }
}