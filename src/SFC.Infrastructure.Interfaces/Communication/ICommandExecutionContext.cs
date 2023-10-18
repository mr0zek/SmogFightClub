namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface ICommandExecutionContext<T>
    where T : ICommand
  {
    T Command { get;  }
    Exception Exception { get; }
    ICommandHandler<T> Handler { get; }
  }
}