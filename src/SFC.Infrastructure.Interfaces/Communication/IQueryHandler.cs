using MediatR;

namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>  
  where TRequest : IRequest<TResponse>
  where TResponse : IResponse
  {    
  }

}
