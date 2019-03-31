using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SFC.Infrastructure
{
  public class FluentValidationActionFilter : IActionFilter
  {
    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!filterContext.ModelState.IsValid)
      {
        filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
      }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {

    }
  }
}