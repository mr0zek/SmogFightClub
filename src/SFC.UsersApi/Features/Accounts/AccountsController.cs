using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.UserApi.Features.Accounts
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public AccountsController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public IActionResult PostAccount([FromBody]PostAccountModel model)
    {
      string id = Guid.NewGuid().ToString().Replace("-","");

      _commandBus.Send(new RegisterUserCommand()
      {
        Id = id.ToString(),
        BaseUrl = BaseUrl.Current,
        LoginName = model.LoginName,
        ZipCode = model.ZipCode,
        Email = model.Email
      });

      return Created(new Uri($"{BaseUrl.Current}/api/v1.0/accounts/{id}"),id);
    }

    [HttpPost("{id}/confirmations")]
    public IActionResult PostConfirmation([FromRoute]string id)
    {
      try
      {
        _commandBus.Send(new ConfirmUserCommand()
        {
          ConfirmationId = id
        });
      }
      catch (InvalidOperationException e)
      {
        return BadRequest();
      }
      
      return Ok();
    }
  }
}