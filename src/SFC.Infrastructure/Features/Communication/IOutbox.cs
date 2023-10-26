using System.Collections;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Communication
{
  interface IOutbox
  {
    void Add(EventData eventData);
    IEnumerable<EventData> Get(int lastProcessedId, int count);
  }
}