using Microsoft.AspNetCore.Mvc;
using SFC.Notifications.Features.NotificationQuery;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private readonly INotificationPerspective _notificationPerspective;

    public NotificationDashboardController(INotificationPerspective notificationPerspective)
    {
      _notificationPerspective = notificationPerspective;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] NotificationDashboardQueryModel query)
    {
      return Json(_notificationPerspective.GetAllSendNotificationsByUser(query.Top, query.Take));
    }
  }
}
