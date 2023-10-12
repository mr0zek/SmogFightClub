namespace SFC.Infrastructure.Interfaces
{
  public interface IQueryHandlerAction<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    void BeforeHandleQuery(TRequest query, IQueryHandler<TRequest,TResponse> handler);
    void AfterHandleQuery(TResponse response);
  }

}
