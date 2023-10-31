using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Communication.AsyncEventProcessing
{
  interface IOutbox
  {
    Task Add(EventData eventData);
    Task<IEnumerable<EventData>> Get(int lastProcessedId, int count);
  }
}