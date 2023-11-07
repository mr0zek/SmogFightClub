namespace MediatR.Asynchronous
{
  public interface IAsyncSender 
  {
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest;

    Task Send(object request, CancellationToken cancellationToken = default);
  }
}