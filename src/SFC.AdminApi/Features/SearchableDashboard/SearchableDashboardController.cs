﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SFC.AdminApi.Features.SearchableDashboard
{
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
    public IActionResult Get([FromQuery]SearchableDashboardQueryModel query)
    {
      var result = _searchableDashboardPerspective.Search(query);
      
      return Json(result);
    }
  }
}