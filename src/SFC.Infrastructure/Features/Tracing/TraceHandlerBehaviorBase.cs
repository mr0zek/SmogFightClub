using MediatR;
using System;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  internal class TraceHandlerBehaviorBase<TRequest, TResponse>
  {
    private readonly ICallStack _callStack;
    
    public TraceHandlerBehaviorBase(ICallStack callStack)
    {
      _callStack = callStack;
    }

    public async Task<T> Run<T>(TRequest request, RequestHandlerDelegate<T> next)
    {
      var moduleName = request.GetType().Assembly.GetName().Name;
      string callType = "Query";
      if (request.GetType().IsAssignableTo(typeof(Interfaces.Communication.ICommand)))
      {
        callType = "Command";
      }
      if (request.GetType().IsAssignableTo(typeof(Interfaces.Communication.IEvent)))
      {
        callType = "Event";
      }
      await _callStack.StartCall(moduleName, typeof(TRequest).Name, callType);

      try
      {
        var response = await next();
        if (response != null)
        {
          await _callStack.FinishCall(response.GetType().Name);
        }
        else
        {
          await _callStack.FinishCall("null");
        }
        return response;
      }
      catch (Exception ex)
      {
        await _callStack.FinishCall(ex.GetType().Name);
        throw;
      }
    }
  }
}
