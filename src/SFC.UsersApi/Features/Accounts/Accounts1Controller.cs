using System;
using System.Threading.Tasks;
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
    public async Task<IActionResult> PostAccount([FromBody]PostAccountModel model)
    {
      Guid id = Guid.NewGuid();

      try
      {
        await _commandBus.Send(new RegisterUserCommand(
          (model.LoginName).ThrowIfNull(), 
          (model.Email).ThrowIfNull(), 
          PasswordHash.FromPassword((model.Password).ThrowIfNull()),
          (model.ZipCode).ThrowIfNull(), 
          Request.BaseUrl(), 
          id));
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
    public async Task<IActionResult> PostConfirmation([FromRoute]Guid id)
    {
      try
      {
        await _commandBus.Send(new ConfirmUserCommand()
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