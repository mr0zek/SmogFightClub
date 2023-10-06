using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public ActionResult<SearchableDashboardResult> Get([FromQuery]SearchableDashboardQueryModel query)
    {
      return _searchableDashboardPerspective.Search(query);
    }
  }
}