using MediatR.Asynchronous;

namespace Microsoft.Extensions.DependencyInjection
{
  public class MediatRAsynchronousServiceConfiguration
  {
    public Func<IServiceProvider, IInboxRepository>? InboxRepository { get; set; }
    public Func<IServiceProvider, IOutboxRepository>? OutboxRepository { get; set; }
  }
}