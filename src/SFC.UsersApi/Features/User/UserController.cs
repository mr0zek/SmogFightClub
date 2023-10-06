using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Sensors.Features.GetAllSensors;
using SFC.Sensors.Features.GetSensor;
using SFC.Sensors.Features.RegisterSensor.Contract;

namespace SFC.UserApi.Features.User
{
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IIdentityProvider _identityProvider;

    public UserController(ICommandBus commandBus, IIdentityProvider identityProvider)
    {
      _commandBus = commandBus;
      _identityProvider = identityProvider;
    }

    [HttpPost]
    public IActionResult Post([FromBody] PostUserModel model)
    {
      _commandBus.Send(new SetNotificationEmailCommand(model.Email, _identityProvider.GetLoginName()));

      return Ok();
    }
  }
}