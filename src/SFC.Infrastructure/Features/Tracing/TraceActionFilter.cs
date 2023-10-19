using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  public class TraceActionFilter : ITraceActionFilter
  {
    private readonly ICallStack _context;

    public TraceActionFilter(ICallStack context)
    {
      _context = context;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
      _context.FinishCall(context.HttpContext.Response.StatusCode.ToString());
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      string methodname = context.HttpContext.Request.Path;
      foreach (var arg in context.ActionArguments)
      {
        methodname = methodname.Replace(arg.Value.ToString(), $"{{{arg.Key}}}");
      }
      _context.StartCall(context.Controller.GetType().Assembly.GetName().Name, methodname, context.HttpContext.Request.Method);
    }
  }
}
