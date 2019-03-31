namespace SFC.Infrastructure
{
  public interface ICommandHandler<T>
  {
    void Handle(T command);
  }
}