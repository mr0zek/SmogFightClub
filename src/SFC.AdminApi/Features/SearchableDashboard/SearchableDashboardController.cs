using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SFC.Accounts.Features.AccountQuery;
using SFC.AdminApi.Features.Dashboard;
using SFC.Notifications.Features.NotificationQuery;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchableDashboardController : Controller
  {
    private readonly ISearchabelDashboardPerspective _searchableDashboardPerspective;

    public SearchableDashboardController(ISearchabelDashboardPerspective searchableDashboardPerspective)
    {
      _searchableDashboardPerspective = searchableDashboardPerspective;
    }

    [HttpGet]
    public IActionResult Get(SearchableDashboardQueryModel query)
    {
      var result = _searchableDashboardPerspective.Search(query);
      
      return Json(result);
    }
  }
}