using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  public class CallStack : ICallStack
  {
    private readonly string _correlationId = Guid.NewGuid().ToString();
    private readonly ITraceRepository _traceRepository;
    private readonly Stack<Call> _callStack = new Stack<Call>();

    public CallStack(ITraceRepository traceRepository)
    {
      _traceRepository = traceRepository;
    }      

    public void StartCall(string moduleName, string callName, string type)
    {      
      if(_callStack.Count == 0)
      {
        _traceRepository.BeginRequest(_correlationId);
      }

      var callingModuleName = _callStack.Count == 0 ? "" : _callStack.Peek().ModuleName;
      _callStack.Push(new Call(moduleName, callName, type));

      _traceRepository.AddCall(new Trace(_correlationId, callName, callingModuleName, moduleName, type));
    }

    public void FinishCall(string callName)
    {
      var callingModuleName = _callStack.Pop().ModuleName;
      string calledModuleName = null;
      if (_callStack.Count > 0)
      {
        calledModuleName = _callStack.Peek().ModuleName;
      }
      _traceRepository.AddCall(new Trace(_correlationId, callName, callingModuleName, calledModuleName, "Return" ));

      if (_callStack.Count == 0)
      {
        _traceRepository.EndRequest(_correlationId);
      }
    }
  }
}
