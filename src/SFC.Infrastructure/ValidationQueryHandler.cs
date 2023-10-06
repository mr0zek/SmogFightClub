using Autofac;
using FluentValidation;
using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Infrastructure
{
  internal class ValidationQueryHandler<TRequest, TResponse> : IQueryHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResponse
  {
    private readonly IQueryHandler<TRequest, TResponse> _queryHandler;
    private readonly IComponentContext _container;

    public ValidationQueryHandler(IQueryHandler<TRequest, TResponse> queryHandler, IComponentContext container)
    {
      _queryHandler = queryHandler;
      _container = container;
    }

    public TResponse HandleQuery(TRequest query)
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

      return _queryHandler.HandleQuery(query);  
    }
  }
}
