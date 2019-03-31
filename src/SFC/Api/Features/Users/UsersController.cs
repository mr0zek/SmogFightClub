using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure;
using SFC.Processes.Contract;
using SFC.Processes.Contract.Command;

namespace SFC.Api.Features.Users
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly ICommandBus _commandBus;

    public UsersController(ICommandBus commandBus)
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