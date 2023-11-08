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
    public static List<int> Requests { get; internal set; } = new List<int>();

    public Task Handle(Ping request, CancellationToken cancellationToken)
    {
      Requests.Add(request.Index);
      return Task.CompletedTask;
    }
  }
}