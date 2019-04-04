using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Features.AlertQuery;
using SFC.Alerts.Features.RegisterAlert.Contract;
using SFC.Infrastructure;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Alerts
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
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterAlertCommand()
      {
        Id = id,
        LoginName = GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Created($"/api/alerts/{id}",id);
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_alertsPerspective.GetAll(GetLoginName()));
    }

    private LoginName GetLoginName()
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