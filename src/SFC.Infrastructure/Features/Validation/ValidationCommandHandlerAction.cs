using Autofac;
using FluentValidation;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Validation
{
  internal class ValidationCommandHandlerAction<T> : ICommandHandlerAction<T>
  {
    private readonly IComponentContext _container;

    public ValidationCommandHandlerAction(IComponentContext container)
    {
      _container = container;
    }

    public void AfterHandle()
    {
    }

    public void BeforeHandle(T command, ICommandHandler<T> handler)
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
    }
  }
}
