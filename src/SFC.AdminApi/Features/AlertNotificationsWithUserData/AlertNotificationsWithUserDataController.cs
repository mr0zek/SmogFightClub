using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.AdminApi.Features.AlertNotificationsWithUserData
{
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/admin/[controller]")]
  [ApiController]
  public class AlertNotificationsWithUserDataController : Controller
  {
    private readonly IDashboardPerspective _dashboardPerspective;

    public AlertNotificationsWithUserDataController(IDashboardPerspective dashboardPerspective)
    {
      _dashboardPerspective = dashboardPerspective;
    }

    [EntryPointFor("Admin", CallerType.Human, CallType.Query)]
    [HttpGet]
    public ActionResult<DashboardResult> Get([FromQuery] AlertNotificationsWithUserDataModel query)
    {
      return _dashboardPerspective.Search(query);
    }
  }
}