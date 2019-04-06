using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFC.Accounts.Features.AccountQuery;
using SFC.Notifications.Features.NotificationQuery;

namespace SFC.AdminApi.Features.Dashboard
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class DashboardController : Controller
  {
    private readonly IDashboardPerspective _dashboardPerspective;

    public DashboardController(IDashboardPerspective dashboardPerspective)
    {
      _dashboardPerspective = dashboardPerspective;
    }

    [HttpGet]
    public IActionResult Get([FromQuery]DashboardQueryModel query)
    {
      return Json(_dashboardPerspective.Search(query));
    }
  }
}