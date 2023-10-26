namespace SFC.Infrastructure.Interfaces.Communication
{
  public class GetEventsRequest<T> : IRequest<GetEventsResponse>
  {
    public int Count { get; set; }

    public int LastProcessedId { get; set; }
  }
}