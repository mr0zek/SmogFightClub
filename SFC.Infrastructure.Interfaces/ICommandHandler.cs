namespace SFC.Infrastructure.Interfaces
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}