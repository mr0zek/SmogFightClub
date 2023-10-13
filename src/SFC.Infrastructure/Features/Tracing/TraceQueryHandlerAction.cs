using SFC.Infrastructure.Interfaces;
using System.Diagnostics;
using System.Threading;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceQueryHandlerAction<TRequest, TResponse> : IQueryHandlerAction<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    private readonly IExecutionContext _executionContext;

    public TraceQueryHandlerAction(IExecutionContext executionContext)
    {
      _executionContext = executionContext;
    }

    public void AfterHandleQuery(TResponse response)
    {
      _executionContext.FinishCall();
    }

    public void BeforeHandleQuery(TRequest query, IQueryHandler<TRequest, TResponse> handler)
    {
      _executionContext.StartCall(
        handler.GetType().Assembly.GetName().Name,
        typeof(TRequest).Name);
    }
  }
}
