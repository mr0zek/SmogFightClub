using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Infrastructure.Interfaces
{
  public interface ICommandHandlerAction<T>
  {
    void BeforeHandle(T command, ICommandHandler<T> handler);
    void AfterHandle();
  }
}