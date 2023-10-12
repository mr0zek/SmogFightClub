using SFC.Infrastructure.Interfaces;
using System.Diagnostics;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceCommandHandlerAction<T> : ICommandHandlerAction<T>
  {
    private readonly ITraceRepository _traceRepository;
    private readonly IExecutionContext _executionContext;

    public TraceCommandHandlerAction(ITraceRepository traceRepository, IExecutionContext executionContext)
    {
      _traceRepository = traceRepository;
      _executionContext = executionContext;
    }

    public void AfterHandle()
    {
      _executionContext.FinishCall();
    }

    public void BeforeHandle(T command, ICommandHandler<T> handler)
    {
      _executionContext.StartCall(
        handler.GetType().Assembly.GetName().Name,
        typeof(T).Name);
    }
  }
}
