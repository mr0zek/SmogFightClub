using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces.Communication;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{

  class QueryTraceHandlerBehavior<TRequest, TResponse> : TraceHandlerBehaviorBase<TRequest, TResponse>, IPipelineBehavior<TRequest, TResponse>
  where TRequest : Interfaces.Communication.IRequest<TResponse>
  where TResponse : IResponse
  {
    public QueryTraceHandlerBehavior(ICallStack callStack) : base(callStack)
    {
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
      return await Run(request, next);
    }
  }
}
