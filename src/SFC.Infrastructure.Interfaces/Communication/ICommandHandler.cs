using MediatR;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface ICommandHandler<T> : IRequestHandler<T>
    where T : ICommand
  {    
  }
}