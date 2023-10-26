using System.Collections.Generic;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public class GetEventsResponse : IResponse
  {
    public IEnumerable<IEvent> Events { get; set; }
  }
}