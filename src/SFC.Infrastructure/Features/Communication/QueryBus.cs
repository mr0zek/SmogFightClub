using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using SFC.Infrastructure.Interfaces;

namespace SFC.Infrastructure.Features.Communication
{
  class QueryBus : IQuery
  {
    private readonly IComponentContext _container;

    public QueryBus(IComponentContext container)
    {
      _container = container;
    }

    public TResponse Query<TResponse>(IRequest<TResponse> request) where TResponse : IResponse
    {
      Type generic = typeof(IQueryHandler<,>);
      generic = generic.MakeGenericType(request.GetType(), typeof(TResponse));

      Type genericAction = typeof(IQueryHandlerAction<,>);
      genericAction = genericAction.MakeGenericType(request.GetType(), typeof(TResponse));

      Type enumerable = typeof(IEnumerable<>);
      enumerable = enumerable.MakeGenericType(genericAction);

      var queryHandler = _container.Resolve(generic);

      IEnumerable actions = (IEnumerable)_container.Resolve(enumerable);
      foreach (var action in actions)
      {
        action.GetType().InvokeMember("BeforeHandleQuery", System.Reflection.BindingFlags.InvokeMethod, null, action, new[] { request, queryHandler });
      }
      
      var result = (TResponse)queryHandler.GetType().InvokeMember("HandleQuery", System.Reflection.BindingFlags.InvokeMethod, null, queryHandler, new[] { request });

      foreach (var action in actions)
      {
        action.GetType().InvokeMember("AfterHandleQuery", System.Reflection.BindingFlags.InvokeMethod, null, action, new object[] { result });
      }

      return result;
    }
  }
}
