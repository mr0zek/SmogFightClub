using MediatR;
using MediatR.Asynchronous;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IEventBus : IAsyncPublisher
  {    
  }
}
