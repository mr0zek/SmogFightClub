using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  public class TraceActionFilter : IActionFilter
  {
    private readonly IExecutionContext _context;

    public TraceActionFilter(IExecutionContext context)
    {
      _context = context;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      _context.FinishCall();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      _context.StartCall(context.Controller.GetType().Assembly.GetName().Name, context.HttpContext.Request.Path);
    }
  }
}
