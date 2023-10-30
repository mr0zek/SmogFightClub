using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Tests.Tools.Mocks
{
  internal class TestEventHandler<T> : IEventHandler<T>
  where T : IEvent
  {
    public static List<T> Events { get; set; } = new List<T>();

    public async Task Handle(T @event, CancellationToken cancellationToken)
    {
      Events.Add(@event);
    }
  }
}