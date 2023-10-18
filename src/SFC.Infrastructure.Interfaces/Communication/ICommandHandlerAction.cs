namespace SFC.Infrastructure.Interfaces.Communication
{
    public interface ICommandHandlerAction<T>
      where T : ICommand
    {
        void BeforeHandle(ICommandExecutionContext<T> executionContext);
        void AfterHandle(ICommandExecutionContext<T> executionContext);
    }
}