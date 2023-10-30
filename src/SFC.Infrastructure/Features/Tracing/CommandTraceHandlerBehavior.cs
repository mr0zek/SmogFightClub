using MediatR;
using SFC.Infrastructure.Interfaces.Communication;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  class CommandTraceHandlerBehavior<TRequest, TResponse> : TraceHandlerBehaviorBase<TRequest, Unit>, IPipelineBehavior<TRequest, Unit>
  where TRequest : Interfaces.Communication.IRequest<TResponse>
  where TResponse : IResponse
  {
    public CommandTraceHandlerBehavior(ICallStack callStack) : base(callStack)
    {
    }

    public async Task<Unit> Handle(TRequest request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
    {
      return await Run<Unit>(request, next);
    }    
  }
}
