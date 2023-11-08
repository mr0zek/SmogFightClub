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
    public static List<int> Requests { get; set; } = new List<int>();
    public Task Handle(Pinged notification, CancellationToken cancellationToken)
    {
      Requests.Add(notification.Index);
      return Task.CompletedTask;
    }
  }
}