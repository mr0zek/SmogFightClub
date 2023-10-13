namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IQuery
  {
    TResponse Query<TResponse>(IRequest<TResponse> request) where TResponse : IResponse;
  }
}
