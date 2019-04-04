using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.AdminApi.Features.Dashboard
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashboardController : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public DashboardController(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    [HttpPost]
    public IActionResult Post(PostUserModel model)
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