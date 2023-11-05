namespace MediatR.Asynchronous
{
  public interface IAsyncSender 
  {
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken) where TRequest : IRequest;

    Task Send(object request, CancellationToken cancellationToken);
  }
}