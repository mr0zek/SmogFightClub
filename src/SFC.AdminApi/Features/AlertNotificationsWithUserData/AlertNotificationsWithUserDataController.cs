using System.Linq;
using System.Threading.Tasks;
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
    public async Task<ActionResult<DashboardResponse>> Get([FromQuery] AlertNotificationsWithUserRequest query)
    {
      return await _dashboardPerspective.Search(query);
    }
  }
}