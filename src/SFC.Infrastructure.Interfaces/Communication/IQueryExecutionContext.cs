namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IQueryExecutionContext<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
        TRequest Request { get; }
        TResponse Response { get; }
        Exception Exception { get; }
        IQueryHandler<TRequest,TResponse> Handler { get; }
    }
}