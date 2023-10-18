using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;

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
      Type handlerType = typeof(IQueryHandler<,>);
      handlerType = handlerType.MakeGenericType(request.GetType(), typeof(TResponse));

      Type genericActionType = typeof(IQueryHandlerAction<,>);
      genericActionType = genericActionType.MakeGenericType(request.GetType(), typeof(TResponse));

      Type enumerable = typeof(IEnumerable<>);
      enumerable = enumerable.MakeGenericType(genericActionType);

      Type executionContextType = typeof(QueryExecutionContext<,>);
      executionContextType = executionContextType.MakeGenericType(request.GetType(), typeof(TResponse));

      var executionContext = Activator.CreateInstance(executionContextType);

      var queryHandler = _container.Resolve(handlerType);

      executionContextType.InvokeMember("Handler", System.Reflection.BindingFlags.SetProperty, null, executionContext, new[] { queryHandler });
      executionContextType.InvokeMember("Request", System.Reflection.BindingFlags.SetProperty, null, executionContext, new[] { request });

      IEnumerable actions = (IEnumerable)_container.Resolve(enumerable);
      foreach (var action in actions)
      {
        action.GetType().InvokeMember("BeforeHandle", System.Reflection.BindingFlags.InvokeMethod, null, action, new[] { executionContext });
      }

      try
      {
        TResponse response = (TResponse)queryHandler.GetType().InvokeMember("HandleQuery", System.Reflection.BindingFlags.InvokeMethod, null, queryHandler, new[] { request });
        executionContextType.InvokeMember("Response", System.Reflection.BindingFlags.SetProperty, null, executionContext, new[] { (object)response });

        foreach (var action in actions)
        {
          try
          {
            action.GetType().InvokeMember("AfterHandle", System.Reflection.BindingFlags.InvokeMethod, null, action, new object[] { executionContext });
          }
          catch (Exception ex)
          {
            Log.Error(ex, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
          }
        }

        return response;
      }
      catch (Exception ex)
      {
        executionContextType.InvokeMember("Exception", System.Reflection.BindingFlags.SetProperty, null, executionContext, new[] { ex });

        foreach (var action in actions)
        {
          try
          {
            action.GetType().InvokeMember("AfterHandle", System.Reflection.BindingFlags.InvokeMethod, null, action, new object[] { executionContext });
          }
          catch (Exception ex2)
          {
            Log.Error(ex2, "Exception while processing AfterHandle of action : {action}", action.GetType().Name);
          }
        }
        throw;
      }
    }
  }
}
