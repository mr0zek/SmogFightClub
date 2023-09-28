namespace SFC.Infrastructure.Interfaces
{
  public interface IQuery
  {
    TResult Query<TResult>(IRequest<TResult> request);
  }
}
