using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.UserApi.Features.Accounts
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public AccountsController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public IActionResult Post(PostAccountModel model)
    {
      _commandBus.Send(new RegisterUserCommand()
      {
        BaseUrl = BaseUrl.Current,
        LoginName = model.LoginName,
        ZipCode = model.ZipCode,
        Email = model.Email
      });

      return Accepted();
    }
  }
}