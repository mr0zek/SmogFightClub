using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Alerts.Features.GetAlert;
using SFC.Alerts.Features.GetAllAlerts;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.UserApi.Features.Alerts
{
  [Authorize]
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

      _commandBus.Send(new CreateAlertCommand()
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
      return Json(_query.Query(new GetAllAlertsRequest(_identityProvider.GetLoginName())));
    }    

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute]Guid id)
    {
      return Json(_query.Query(new GetAlertRequest(id, _identityProvider.GetLoginName())));
    }
  }
}