namespace SFC.Infrastructure.Interfaces
{
  public interface IQueryHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
  {
    TResponse HandleQuery(TRequest query);
  }

}
