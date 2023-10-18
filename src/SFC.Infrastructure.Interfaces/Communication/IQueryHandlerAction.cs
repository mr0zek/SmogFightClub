namespace SFC.Infrastructure.Interfaces.Communication
{
    public interface IQueryHandlerAction<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
      where TResponse : IResponse
    {
        void BeforeHandle(IQueryExecutionContext<TRequest, TResponse> executionContext);
        void AfterHandle(IQueryExecutionContext<TRequest, TResponse> executionContext);
    }

}
