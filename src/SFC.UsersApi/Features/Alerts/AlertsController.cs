using System;
using System.Threading.Tasks;
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
    public async Task<IActionResult> Post(PostAlertModel model)
    {
      Guid id = Guid.NewGuid();

      await _commandBus.Send(new CreateAlertCommand(_identityProvider.GetLoginName(), (model.ZipCode).ThrowIfNull(), id));
      
      return Created($"/api/alerts/{id}",id);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Json(await _query.Send(new GetAllAlertsRequest(_identityProvider.GetLoginName())));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]Guid id)
    {
      return Json(await _query.Send(new GetAlertRequest(id, _identityProvider.GetLoginName())));
    }
  }
}