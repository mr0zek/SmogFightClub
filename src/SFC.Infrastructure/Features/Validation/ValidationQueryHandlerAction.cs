using Autofac;
using FluentValidation;
using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Infrastructure.Features.Validation
{
    internal class ValidationQueryHandlerAction<TRequest, TResponse> : IQueryHandlerAction<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    private readonly IComponentContext _container;

    public ValidationQueryHandlerAction(IComponentContext container)
    {
      _container = container;
    }

    public void AfterHandle(IQueryExecutionContext<TRequest, TResponse> executionContext)
    {
    }


    public void BeforeHandle(IQueryExecutionContext<TRequest, TResponse> executionContext)
    {
      if (_container.IsRegistered<IValidator<TRequest>>())
      {
        var validator = _container.Resolve<IValidator<TRequest>>();

        var validationResult = validator.Validate(executionContext.Request);

        if (!validationResult.IsValid)
        {
          throw new ArgumentException(validationResult.ToString(), "request");
        }
      }    
    }
  }
}
