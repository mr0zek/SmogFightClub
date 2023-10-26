using Autofac;
using Newtonsoft.Json;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Features.Communication
{
  class EventBusWithAsync : IEventBusWithAsync
  {
    private readonly IOutbox _outbox;

    public EventBusWithAsync(IOutbox outbox) 
    {
      _outbox = outbox;
    }

    public void Publish<T>(T @event) where T : IEvent
    {
      var data = JsonConvert.SerializeObject(
        @event, 
        new ZipCodeJsonConverter(), 
        new LoginNameJsonConverter());
      var type = typeof(T).AssemblyQualifiedName;
      _outbox.Add(new EventData() { Data = data, Type = type });
    }
  }
}
