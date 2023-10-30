using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.AdminApi.Features.AlertNotificationsWithUserData;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  [Authorize]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/admin/[controller]")]
  [ApiController]  
  public class SearchableDashboardController : Controller
  {
    private readonly ISearchabelDashboardPerspective _searchableDashboardPerspective;

    public SearchableDashboardController(ISearchabelDashboardPerspective searchableDashboardPerspective)
    {
      _searchableDashboardPerspective = searchableDashboardPerspective;
    }

    [EntryPointFor("Admin", CallerType.Human, CallType.Query)]
    [HttpGet]
    public async Task<ActionResult<SearchableDashboardResult>> Get([FromQuery]SearchableDashboardQueryModel query)
    {
      return await _searchableDashboardPerspective.Search(query);
    }
  }
}