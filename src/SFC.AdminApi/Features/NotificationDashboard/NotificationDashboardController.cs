using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.GetAllSendNotificationsByUserQuery.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.AdminApi.Features.NotificationDashboard
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class NotificationDashboardController : Controller
  {
    private readonly IQuery _query;

    public NotificationDashboardController(IQuery query)
    {
      _query = query;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] NotificationDashboardQueryModel query)
    {
      return Json(_query.Query(new GetAllSendNotificationsByUserQuery(query.Top, query.Take)));
    }
  }
}
