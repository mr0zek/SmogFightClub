using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using System.Diagnostics;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceCommandHandlerAction<T> : ICommandHandlerAction<T>
  {
    private readonly IExecutionContext _executionContext;

    public TraceCommandHandlerAction(IExecutionContext executionContext)
    {
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
