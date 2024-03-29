﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
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

    [EntryPointFor("User", CallerType.Human, CallType.Command)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostUserModel model)
    {
      await _commandBus.Send(new SetNotificationEmailCommand((model.Email).ThrowIfNull(), _identityProvider.GetLoginName()));

      return Ok();
    }
  }
}