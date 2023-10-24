using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Infrastructure.Features.Communication
{
  class QueryExecutionContext<TRequest, TResponse> : IQueryExecutionContext<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    public TRequest Request { get; set; }

    public TResponse Response { get; set; }

    public Exception Exception { get; set; }

    public IQueryHandler<TRequest, TResponse> Handler { get; set; }
  }
}