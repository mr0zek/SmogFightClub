namespace SFC.Infrastructure.Interfaces
{
  public interface IQueryBus
  {
    TResult Query<TResult>(IRequest<TResult> request);
  }
}
