using Autofac;
using FluentValidation;
using SFC.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure
{
  internal class ValidationCommandHandler<T> : ICommandHandler<T>
  {
    private readonly ICommandHandler<T> _commandHandler;
    private readonly IComponentContext _container;

    public ValidationCommandHandler(ICommandHandler<T> commandHandler, IComponentContext container)
    {
      _commandHandler = commandHandler;
      _container = container;
    }
    public void Handle(T command)
    {
      if (_container.IsRegistered<IValidator<T>>())
      {
        var validator = _container.Resolve<IValidator<T>>();

        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
          throw new ArgumentException(validationResult.ToString(), nameof(command));
        }
      }

      _commandHandler.Handle(command);  
    }
  }
}
