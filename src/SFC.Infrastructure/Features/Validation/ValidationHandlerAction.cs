using Autofac;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Validation
{
    internal class ValidationHandlerAction<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : Interfaces.Communication.IRequest<TResponse>
    where TResponse : IResponse
  {
    private readonly IComponentContext _container;

    public ValidationHandlerAction(IComponentContext container)
    {
      _container = container;
    }
        
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
      if (_container.IsRegistered<IValidator<TRequest>>())
      {
        var validator = _container.Resolve<IValidator<TRequest>>();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
          throw new ArgumentException(validationResult.ToString(), "request");
        }
      }

      return await next();
    }
  }
}
