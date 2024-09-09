using System.Collections.Concurrent;

namespace MediatR.Asynchronous.Tests
{
  public class Ping : IRequest
  {
    public Ping(int index, string message)
    {
      Index = index;
      Message = message;
    }

    public string Message { get; set; }
    public int Index { get; internal set; }
  }

  public class PingHandler : IRequestHandler<Ping>
  {
    public static ConcurrentBag<int> Requests { get; internal set; } = new ConcurrentBag<int>();

    public Task Handle(Ping request, CancellationToken cancellationToken)
    {
      Thread.Sleep(10);
      Requests.Add(request.Index);
      return Task.CompletedTask;
    }
  }
}