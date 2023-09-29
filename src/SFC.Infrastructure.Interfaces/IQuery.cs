namespace SFC.Infrastructure.Interfaces
{
  public interface IQuery
  {
    TResponse Query<TResponse>(IRequest<TResponse> request);
  }
}
