using Microsoft.AspNetCore.Http;
using SFC.Infrastructure.Interfaces.Tracing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  class CallStack : ICallStack
  {
    private string _correlationId;
    private readonly IRequestLifecycle _requestLifecycle;
    private readonly Stack<Call> _callStack = new Stack<Call>();

    public CallStack(IRequestLifecycle requestLifecycle)
    {
      _requestLifecycle = requestLifecycle;
    }      

    public async Task StartCall(string calledModuleName, string callName, string type, string callingModuleName = null)
    {      
      if(_callStack.Count == 0)
      {
        _correlationId = Guid.NewGuid().ToString();
        await _requestLifecycle.BeginRequest(_correlationId);
      }

      if (callingModuleName == null && _callStack.Count > 0)
      {
        callingModuleName = _callStack.Peek().CalledModuleName;
      }
      _callStack.Push(new Call(calledModuleName, callName, type, callingModuleName));

      await _requestLifecycle.AddModuleCall(new ModuleCall(_correlationId, callName, callingModuleName, calledModuleName, type));
    }

    public async Task FinishCall(string callName)
    {
      var call = _callStack.Pop();
      await _requestLifecycle.AddModuleCall(new ModuleCall(_correlationId, callName, call.CalledModuleName, call.CallingModuleName, "Return" ));

      if (_callStack.Count == 0)
      {
        await _requestLifecycle.EndRequest(_correlationId);
      }
    }
  }
}
