using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFC.Accounts.Features.AccountQuery;
using SFC.Notifications.Features.NotificationQuery;

namespace SFC.AdminApi.Features.Dashboard
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashboardController : Controller
  {
    private readonly IDashboardPerspective _dashboardPerspective;

    public DashboardController(IDashboardPerspective dashboardPerspective)
    {
      _dashboardPerspective = dashboardPerspective;
    }

    [HttpGet]
    public IActionResult Get(DashboardQueryModel query)
    {
      return Json(_dashboardPerspective.Search(query));
    }
  }
}