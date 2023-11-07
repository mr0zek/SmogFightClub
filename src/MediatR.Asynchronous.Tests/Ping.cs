namespace MediatR.Asynchronous.Tests
{
  public class Ping : IRequest
  {
    public Ping(string message)
    {
      Message = message;
    }

    public string Message { get; set; }
  }

  public class PingHandler : IRequestHandler<Ping>
  {
    public static int RequestsCount { get; private set; }
    public Task Handle(Ping request, CancellationToken cancellationToken)
    {
      RequestsCount++;
      return Task.CompletedTask;
    }
  }
}