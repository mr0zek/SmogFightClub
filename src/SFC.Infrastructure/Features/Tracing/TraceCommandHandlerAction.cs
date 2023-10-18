using SFC.Infrastructure.Interfaces.Communication;
using System.Diagnostics;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceCommandHandlerAction<T> : ICommandHandlerAction<T>
  where T : ICommand
  {
    private readonly ICallStack _callStack;

    public TraceCommandHandlerAction(ICallStack callStack)
    {
      _callStack = callStack;
    }

    public void AfterHandle(ICommandExecutionContext<T> executionContext)
    {
      if (executionContext.Exception != null)
      {
        _callStack.FinishCall(executionContext.Exception.GetType().Name);
      }
      else
      {
        _callStack.FinishCall(null);
      }
    }

    public void BeforeHandle(ICommandExecutionContext<T> executionContext)
    {
      _callStack.StartCall(
        executionContext.Handler.GetType().Assembly.GetName().Name,
        typeof(T).Name, "Command");
    }
  }
}
