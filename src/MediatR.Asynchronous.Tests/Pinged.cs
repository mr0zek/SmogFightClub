using System.Collections.Concurrent;

namespace MediatR.Asynchronous.Tests
{
  public class Pinged : INotification
  {
    public Pinged(int index)
    {
      Index = index;
    }

    public int Index { get; }
  }

  public class PingedHandler : INotificationHandler<Pinged>
  {
    public static ConcurrentBag<int> Requests { get; set; } = new ConcurrentBag<int>();
    public Task Handle(Pinged notification, CancellationToken cancellationToken)
    {
      Thread.Sleep(10);
      Requests.Add(notification.Index);
      return Task.CompletedTask;
    }
  }
}