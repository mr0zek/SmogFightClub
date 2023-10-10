using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistration.Contract;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Accounts
{
  [ApiVersion("1.0")]
  [Route("api/v1.0/accounts")]
  [ApiController]
  public class AccountsV1Controller : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public AccountsV1Controller(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public IActionResult PostAccount([FromBody]PostAccountModel model)
    {
      Guid id = Guid.NewGuid();

      try
      {
        _commandBus.Send(new RegisterUserCommand()
        {
          Id = id,
          BaseUrl = BaseUrl.Current,
          LoginName = model.LoginName,
          ZipCode = model.ZipCode,
          Email = model.Email,
          PasswordHash = PasswordHash.FromPassword(model.Password)
        });
      }
      catch (LoginNameAlreadyUsedException)
      {
        var mds = new ModelStateDictionary();
        mds.AddModelError("loginName", "Already exists");
        return BadRequest(mds);
      }

      return Created(new Uri($"{BaseUrl.Current}/api/v1.0/accounts/{id}"),id);
    }

    [HttpPost("{id}/confirmation")]
    public IActionResult PostConfirmation([FromRoute]Guid id)
    {
      try
      {
        _commandBus.Send(new ConfirmUserCommand()
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