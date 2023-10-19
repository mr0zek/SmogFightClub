using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Documentation;
using SFC.Processes.Features.UserRegistration.Contract;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Accounts
{
    [ApiVersion("1.0")]
  [Route("api/v1.0/accounts")]
  [ApiController]  
  public class Accounts1Controller : Controller
  {
    private readonly ICommandBus _commandBus;

    public Accounts1Controller(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [EntryPointFor("User", CallerType.Human, CallType.Command)]
    [HttpPost]
    public IActionResult PostAccount([FromBody]PostAccountModel model)
    {
      Guid id = Guid.NewGuid();

      try
      {
        _commandBus.Send(new RegisterUserCommand()
        {
          Id = id,
          BaseUrl = Request.BaseUrl(),
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

      return Created(Request.BaseUrl($"api/v1.0/accounts/{id}"),id);
    }

    [EntryPointFor("User", CallerType.Human, CallType.Command)]
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