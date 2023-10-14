using Serilog;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceRepository : ITraceRepository
  {
    public void AddCall(Trace trace)
    {
      Log.Information("{@CorrelationId}: {@CallingModuleName} --> {@CalledModuleName} : {@MessageName}",
        trace.CorrelationId,
        trace.CallingModuleName,
        trace.CalledModuleName,
        trace.CallName);
    }

    public void BeginRequest(string correlationId)
    {
    }

    public void EndRequest(string correlationId)
    {
    }
  }
}