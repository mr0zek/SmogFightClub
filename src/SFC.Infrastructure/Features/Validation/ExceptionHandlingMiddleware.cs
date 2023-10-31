using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Validation
{
  internal sealed class ExceptionHandlingMiddleware : IMiddleware
  {
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      try
      {
        await next(context);
      }
      catch (Exception e)
      {
        _logger.LogError(e, e.Message);
        await HandleExceptionAsync(context, e);
      }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
      var statusCode = GetStatusCode(exception);
      var response = new
      {        
        status = statusCode,
        detail = exception.Message,
        errors = GetErrors(exception)
      };
      httpContext.Response.ContentType = "application/json";
      httpContext.Response.StatusCode = statusCode;
      await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception)
    {
      if (exception is ValidationException)
        return StatusCodes.Status422UnprocessableEntity;
      else
        return StatusCodes.Status500InternalServerError;
    }

    private static IReadOnlyDictionary<string, IEnumerable<string>> GetErrors(Exception exception)
    {
      IReadOnlyDictionary<string, IEnumerable<string>> errors = null;
      if (exception is ValidationException validationException)
      {
        errors = validationException.Errors
          .GroupBy(f=>f.PropertyName)
          .ToDictionary(f=>f.Key, f=>f.Select(x=>x.ErrorMessage));
      }
      return errors;
    }
  }
}
