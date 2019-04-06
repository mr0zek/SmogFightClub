using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Features.AlertQuery;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Alerts
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class AlertsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IAlertConditionsPerspective _alertConditionsPerspective;

    public AlertsController(ICommandBus commandBus, IAlertConditionsPerspective alertConditionsPerspective)
    {
      _commandBus = commandBus;
      _alertConditionsPerspective = alertConditionsPerspective;
    }

    [HttpPost]
    public IActionResult Post(PostAlertModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterAlertConditionCommand()
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
      return Json(_alertConditionsPerspective.GetAll(GetLoginName()));
    }

    private LoginName GetLoginName()
    {
      return User.Identity.Name;
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]string id)
    {
      return Json(_alertConditionsPerspective.Get(id, GetLoginName()));
    }
  }
}