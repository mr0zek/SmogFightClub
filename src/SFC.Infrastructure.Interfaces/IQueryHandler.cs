namespace SFC.Infrastructure.Interfaces
{
  public interface IQueryHandler<TRequest, TResult> where TRequest : IRequest<TResult>
  {
    TResult HandleQuery(TRequest query);
  }

}
