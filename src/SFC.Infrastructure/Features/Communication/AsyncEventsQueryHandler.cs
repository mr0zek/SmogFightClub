using Newtonsoft.Json;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Linq;

namespace SFC.Infrastructure.Features.Communication
{
  class AsyncEventsQueryHandler<T> : IQueryHandler<GetEventsRequest<T>, GetEventsResponse>
  {
    private IOutbox _outbox;

    public AsyncEventsQueryHandler(IOutbox outbox)
    {
      _outbox = outbox;
    }

    public GetEventsResponse HandleQuery(GetEventsRequest<T> query)
    {
      var events = _outbox.Get(query.LastProcessedId, query.Count)
        .Select(f=> (IEvent)JsonConvert.DeserializeObject(f.Data, Type.GetType(f.Type)));
      return new GetEventsResponse() { Events = events };
    }
  }
}