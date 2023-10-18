using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
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

      CommandExecutionContext<T> executionContext = new CommandExecutionContext<T>();
      executionContext.Handler = (ICommandHandler<T>)_container.Resolve(typeof(ICommandHandler<T>));
      executionContext.Command = command;


      foreach (var action in actions)
      {
        try
        {
          action.BeforeHandle(executionContext);
        }
        catch (Exception ex)
        {
          Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
        }
      }

      try
      {
        executionContext.Handler.Handle(command);
      }
      catch (Exception ex)
      {
        executionContext.Exception = ex;
      }

      foreach (var action in actions)
      {
        try
        {
          action.AfterHandle(executionContext);
        }
        catch (Exception ex)
        {
          Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
        }
      }

      if (executionContext.Exception != null)
      {
        throw executionContext.Exception;
      }
    }
  }
}
