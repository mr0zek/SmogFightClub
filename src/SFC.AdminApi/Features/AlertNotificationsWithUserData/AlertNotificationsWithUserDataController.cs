using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class AlertNotificationsWithUserDataController : Controller
  {
    private readonly IDashboardPerspective _dashboardPerspective;

    public AlertNotificationsWithUserDataController(IDashboardPerspective dashboardPerspective)
    {
      _dashboardPerspective = dashboardPerspective;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] AlertNotificationsWithUserDataModel query)
    {
      return Json(_dashboardPerspective.Search(query));
    }
  }
}