using Microsoft.AspNetCore.Http;
using SFC.Infrastructure.Interfaces.Tracing;
using System;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  class CallStack : ICallStack
  {
    private readonly string _correlationId = Guid.NewGuid().ToString();
    private readonly IRequestLifecycle _requestLifecycle;
    private readonly Stack<Call> _callStack = new Stack<Call>();

    public CallStack(IRequestLifecycle requestLifecycle)
    {
      _requestLifecycle = requestLifecycle;
    }      

    public void StartCall(string moduleName, string callName, string type)
    {      
      if(_callStack.Count == 0)
      {
        _requestLifecycle.BeginRequest(_correlationId);
      }

      var callingModuleName = _callStack.Count == 0 ? "" : _callStack.Peek().ModuleName;
      _callStack.Push(new Call(moduleName, callName, type));

      _requestLifecycle.AddModuleCall(new ModuleCall(_correlationId, callName, callingModuleName, moduleName, type));
    }

    public void FinishCall(string callName)
    {
      var callingModuleName = _callStack.Pop().ModuleName;
      string calledModuleName = null;
      if (_callStack.Count > 0)
      {
        calledModuleName = _callStack.Peek().ModuleName;
      }
      _requestLifecycle.AddModuleCall(new ModuleCall(_correlationId, callName, callingModuleName, calledModuleName, "Return" ));

      if (_callStack.Count == 0)
      {
        _requestLifecycle.EndRequest(_correlationId);
      }
    }
  }
}
