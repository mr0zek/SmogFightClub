namespace SFC.Infrastructure.Interfaces
{
  public interface IQueryHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : IResponse
  {
    TResponse HandleQuery(TRequest query);
  }

}
