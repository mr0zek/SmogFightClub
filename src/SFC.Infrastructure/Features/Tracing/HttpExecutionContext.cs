using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  public class HttpExecutionContext : IExecutionContext
  {
    private const string _correlationHeaderKey = "CorrelationId";
    private IHttpContextAccessor _httpContextAccessor;
    private readonly ITraceRepository _traceRepository;
    private readonly Stack<Call> _callStack = new Stack<Call>();

    public HttpExecutionContext(IHttpContextAccessor httpContextAccessor, ITraceRepository traceRepository)
    {
      _httpContextAccessor = httpContextAccessor;
      _traceRepository = traceRepository;
    }

    private string CorrelationId
    {
      get
      {
        if (_httpContextAccessor.HttpContext == null)
        {
          throw new InvalidOperationException("Context does not exists");
        }

        if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(_correlationHeaderKey))
        {
          return null;
        }

        return _httpContextAccessor.HttpContext.Request.Headers[_correlationHeaderKey].ToString();
      }
      set
      {
        if (!_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(_correlationHeaderKey))
        {
          _httpContextAccessor.HttpContext.Request.Headers.Add(_correlationHeaderKey, Guid.NewGuid().ToString());
        }
      }
    }

    public void StartCall(string moduleName, string callName)
    {
      if (CorrelationId == null)
      {
        CorrelationId = Guid.NewGuid().ToString();
      }

      var callingModuleName = _callStack.Count == 0 ? "" : _callStack.Peek().ModuleName;
      _callStack.Push(new Call(moduleName, callName));

      _traceRepository.Add(new Trace(CorrelationId, callName, callingModuleName, moduleName));
    }

    public void FinishCall()
    {
      _callStack.Pop();
    }
  }
}
