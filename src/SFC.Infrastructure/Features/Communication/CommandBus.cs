using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Infrastructure.Features.Communication
{
  class CommandBus : ICommandBus
  {
    private readonly IComponentContext _container;

    public CommandBus(IComponentContext container)
    {
      _container = container;
    }

    public void Send<T>(T command) where T : ICommand
    {
      IEnumerable<ICommandHandlerAction<T>> actions = (IEnumerable<ICommandHandlerAction<T>>)_container.Resolve(typeof(IEnumerable<ICommandHandlerAction<T>>));

      ICommandHandler<T> commandHandler = (ICommandHandler<T>)_container.Resolve(typeof(ICommandHandler<T>));

      foreach (var action in actions)
      {
        action.BeforeHandle(command, commandHandler);
      }

      commandHandler.Handle(command);

      foreach (var action in actions)
      {
        action.AfterHandle();
      }
    }
  }
}
