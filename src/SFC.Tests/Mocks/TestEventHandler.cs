using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using System.Collections.Generic;

namespace SFC.Tests.Mocks
{
    internal class TestEventHandler<T> : IEventHandler<T>
  {
    public static List<T> Events { get; set; } = new List<T>();
    
    public void Handle(T @event)
    {
      Events.Add(@event);
    }
  }
}