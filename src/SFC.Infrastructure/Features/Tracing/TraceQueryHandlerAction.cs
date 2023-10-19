using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Tracing;
using System.Diagnostics;
using System.Threading;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceQueryHandlerAction<TRequest, TResponse> : IQueryHandlerAction<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : IResponse
  {
    private readonly ICallStack _callStack;

    public TraceQueryHandlerAction(ICallStack callStack)
    {
      _callStack = callStack;
    }

    public void AfterHandle(IQueryExecutionContext<TRequest, TResponse> executionContext)
    {
      if (executionContext.Exception != null)
      {
        _callStack.FinishCall(executionContext.Exception.GetType().Name);
      }
      else
      {
        if (executionContext.Response != null)
        {
          _callStack.FinishCall(executionContext.Response.GetType().Name);
        }
        else
        {
          _callStack.FinishCall("null");
        }
      }
    }

    public void BeforeHandle(IQueryExecutionContext<TRequest, TResponse> executionContext)
    {
      _callStack.StartCall(
        executionContext.Handler.GetType().Assembly.GetName().Name, typeof(TRequest).Name, "Query");
    }
  }
}
