namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}