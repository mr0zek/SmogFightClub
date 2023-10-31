using Autofac;
using Newtonsoft.Json;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{
  class EventBusWithAsync : IEventBusWithAsync
  {
    private readonly IOutbox _outbox;

    public EventBusWithAsync(IOutbox outbox)
    {
      _outbox = outbox;
    }

    public async Task Publish<T>(T @event) where T : IEvent
    {
      var data = JsonConvert.SerializeObject(
        @event,
        new ZipCodeJsonConverter(),
        new LoginNameJsonConverter());
      var type = typeof(T).AssemblyQualifiedName;
      await _outbox.Add(new EventData() { Data = data, Type = type });
    }
  }
}
