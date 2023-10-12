using SFC.Infrastructure.Interfaces;
using System.Diagnostics;
using System.Threading;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceQueryHandlerAction<TRequest, TResponse> : IQueryHandlerAction<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    private readonly ITraceRepository _traceRepository;
    private readonly IExecutionContext _executionContext;

    public TraceQueryHandlerAction(ITraceRepository traceRepository, IExecutionContext executionContext)
    {
      _traceRepository = traceRepository;
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
