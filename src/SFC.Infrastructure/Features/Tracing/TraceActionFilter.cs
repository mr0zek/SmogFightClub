﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces.Documentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceActionFilter : IActionFilter
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
      var callingModuleName = (context.ActionDescriptor as ControllerActionDescriptor).ThrowIfNull()
        .MethodInfo?
        .CustomAttributes?
        .First(f => f.AttributeType == typeof(EntryPointForAttribute))?
        .ConstructorArguments[0].Value?.ToString();
      string methodname = context.HttpContext.Request.Path;
      foreach (var arg in context.ActionArguments)
      {
        methodname = methodname.Replace((arg.Value?.ToString()).ThrowIfNull(), $"{{{arg.Key}}}");
      }
      _context.StartCall(context.Controller.GetType().Assembly.GetName().Name.ThrowIfNull(), methodname, context.HttpContext.Request.Method, callingModuleName.ThrowIfNull());
    }
  }
}
