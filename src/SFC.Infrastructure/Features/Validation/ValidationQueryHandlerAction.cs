using Autofac;
using FluentValidation;
using SFC.Infrastructure.Interfaces;
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

    public void AfterHandleQuery(TResponse response)
    {
    }


    public void BeforeHandleQuery(TRequest query, IQueryHandler<TRequest, TResponse> handler)
    {
      if (_container.IsRegistered<IValidator<TRequest>>())
      {
        var validator = _container.Resolve<IValidator<TRequest>>();

        var validationResult = validator.Validate(query);

        if (!validationResult.IsValid)
        {
          throw new ArgumentException(validationResult.ToString(), nameof(query));
        }
      }    
    }
  }
}
