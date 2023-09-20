using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Features.AlertQuery;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
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
    private readonly IIdentityProvider _identityProvider;

    public AlertsController(ICommandBus commandBus, IAlertConditionsPerspective alertConditionsPerspective, IIdentityProvider identityProvider)
    {
      _commandBus = commandBus;
      _alertConditionsPerspective = alertConditionsPerspective;
      _identityProvider = identityProvider;
    }

    [HttpPost]
    public IActionResult Post(PostAlertModel model)
    {
      Guid id = Guid.NewGuid();

      _commandBus.Send(new RegisterAlertConditionCommand()
      {
        Id = id,
        LoginName = _identityProvider.GetLoginName(),
        ZipCode = model.ZipCode
      });

      return Created($"/api/alerts/{id}",id);
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Json(_alertConditionsPerspective.GetAll(_identityProvider.GetLoginName()));
    }    

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]string id)
    {
      return Json(_alertConditionsPerspective.Get(id, _identityProvider.GetLoginName()));
    }
  }
}