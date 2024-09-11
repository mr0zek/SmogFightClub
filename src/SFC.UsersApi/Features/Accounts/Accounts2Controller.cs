using System;
using System.Threading.Tasks;
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
  public class Accounts2Controller : Controller
  {
    private readonly ICommandBus _commandBus;

    public Accounts2Controller(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public async Task<IActionResult> PostAccount([FromBody] PostAccountModel model)
    {
      string id = Guid.NewGuid().ToString().Replace("-", "");

      try
      {
        await _commandBus.Send(new RegisterUserCommandSaga(
          (model?.LoginName).ThrowIfNull(), 
          (model?.Email).ThrowIfNull(), 
          PasswordHash.FromPassword((model?.Password).ThrowIfNull()), 
          (model?.ZipCode).ThrowIfNull(), 
          (Request?.BaseUrl("")).ThrowIfNull(), 
          id));        
      }
      catch (LoginNameAlreadyUsedSagaException)
      {
        var mds = new ModelStateDictionary();
        mds.AddModelError("loginName", "Already exists");
        return BadRequest(mds);
      }

      return Created(new Uri((Request?.BaseUrl($"api/v2.0/accounts/{id}")).ThrowIfNull()), id);
    }

    [HttpPost("{id}/confirmation")]
    public async Task<IActionResult> PostConfirmation([FromRoute] string id)
    {
      try
      {
        await _commandBus.Send(new ConfirmUserCommandSaga(id));
      }
      catch (InvalidOperationException)
      {
        return BadRequest();
      }

      return Ok();
    }
  }
}