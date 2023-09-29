using System;
using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Features.GetAllAlertCondition;
using SFC.Alerts.Features.GetAllAlertConditions;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.UserApi.Features.Alerts
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/user/[controller]")]
  [ApiController]
  public class AlertsController : Controller
  {
    private readonly ICommandBus _commandBus;
    private readonly IIdentityProvider _identityProvider;
    private readonly IQuery _query;

    public AlertsController(ICommandBus commandBus, IIdentityProvider identityProvider, IQuery query)
    {
      _commandBus = commandBus;
      _identityProvider = identityProvider;
      _query = query;
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
      return Json(_query.Query(new GetAllAlertConditionsRequest(_identityProvider.GetLoginName())));
    }    

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]string id)
    {
      return Json(_query.Query(new GetAlertConditionRequest(id, _identityProvider.GetLoginName())));
    }
  }
}