using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  public class ExecutionContext : IExecutionContext
  {
    private readonly string _correlationId = Guid.NewGuid().ToString();
    private readonly ITraceRepository _traceRepository;
    private readonly Stack<Call> _callStack = new Stack<Call>();

    public ExecutionContext(ITraceRepository traceRepository)
    {
      _traceRepository = traceRepository;
    }      

    public void StartCall(string moduleName, string callName)
    {      
      if(_callStack.Count == 0)
      {
        _traceRepository.BeginRequest(_correlationId);
      }

      var callingModuleName = _callStack.Count == 0 ? "" : _callStack.Peek().ModuleName;
      _callStack.Push(new Call(moduleName, callName));

      _traceRepository.AddCall(new Trace(_correlationId, callName, callingModuleName, moduleName));
    }

    public void FinishCall()
    {
      _callStack.Pop();

      if (_callStack.Count == 0)
      {
        _traceRepository.EndRequest(_correlationId);
      }
    }
  }
}
