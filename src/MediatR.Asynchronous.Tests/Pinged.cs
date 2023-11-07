namespace MediatR.Asynchronous.Tests
{
  public class Pinged : INotification
  {
  }

  public class PingedHandler : INotificationHandler<Pinged>
  {
    public static int RequestsCount { get; private set; }
    public Task Handle(Pinged notification, CancellationToken cancellationToken)
    {
      RequestsCount++;
      return Task.CompletedTask;
    }
  }
}