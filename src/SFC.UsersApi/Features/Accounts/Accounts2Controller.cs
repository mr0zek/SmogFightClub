﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Processes.Features.UserRegistrationSaga.Contract;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Accounts
{
    [Route("api/v2.0/accounts")]
  [ApiController]
  public class Accounts2Controller : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public Accounts2Controller(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public IActionResult PostAccount([FromBody]PostAccountModel model)
    {
      string id = Guid.NewGuid().ToString().Replace("-","");

      try
      {
        _commandBus.Send(new RegisterUserCommandSaga()
        {
          Id = id,
          BaseUrl = BaseUrl.Current,
          LoginName = model.LoginName,
          ZipCode = model.ZipCode,
          Email = model.Email,
          PasswordHash = PasswordHash.FromPassword(model.Password)
        });
      }
      catch (LoginNameAlreadyUsedSagaException)
      {
        var mds = new ModelStateDictionary();
        mds.AddModelError("loginName", "Already exists");
        return BadRequest(mds);
      }

      return Created(new Uri($"{BaseUrl.Current}/api/v2.0/accounts/{id}"),id);
    }

    [HttpPost("{id}/confirmation")]
    public IActionResult PostConfirmation([FromRoute]string id)
    {
      try
      {
        _commandBus.Send(new ConfirmUserCommandSaga()
        {
          ConfirmationId = id
        });
      }
      catch (InvalidOperationException)
      {
        return BadRequest();
      }
      
      return Ok();
    }
  }
}