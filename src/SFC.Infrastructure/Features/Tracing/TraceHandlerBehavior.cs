using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{

  class TraceHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : class  
  {
    ICallStack _callStack;

    public TraceHandlerBehavior(ICallStack callStack) 
    {
      _callStack = callStack;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
      var moduleName = request.GetType().Assembly.GetName().Name;
      string callType = "Query";
      if (request.GetType().IsAssignableTo(typeof(Interfaces.Communication.ICommand)))
      {
        callType = "Command";
      }      
      await _callStack.StartCall(moduleName.ThrowIfNull(), typeof(TRequest).Name, callType, "");

      try
      {
        var response = await next();
        if (response != null && response.GetType() != typeof(Unit))
        {
          await _callStack.FinishCall(response.GetType().Name);
        }
        else
        {
          await _callStack.FinishCall("retrun");
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
