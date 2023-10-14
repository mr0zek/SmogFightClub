using SFC.Infrastructure.Features.Tracing;

namespace SFC.Tests.UserApi
{
  internal class MyTraceRepository : ITraceRepository
  {
    
    public void AddCall(Trace trace)
    {
    }


    public void BeginRequest(string correlationId)
    {
    }


    public void EndRequest(string correlationId)
    {
    }
  }
}