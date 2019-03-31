using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Contract.Command;
using SFC.Alerts.Contract.Query;
using SFC.Infrastructure;

namespace SFC.Api.Features.Alerts
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlertsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IAlertsPerspective _alertsPerspective;

    public AlertsController(ICommandBus commandBus, IAlertsPerspective alertsPerspective)
    {
      _commandBus = commandBus;
      _alertsPerspective = alertsPerspective;
    }

    [HttpPost]
    public IActionResult Post(PostAlertModel model)
    {
      _commandBus.Send(new RegisterAlertCommand()
      {
        LoginName = GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_alertsPerspective.GetAll(GetLoginName()));
    }

    private string GetLoginName()
    {
      return User.Identity.Name;
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
      return Json(_alertsPerspective.Get(id, GetLoginName()));
    }
  }
}