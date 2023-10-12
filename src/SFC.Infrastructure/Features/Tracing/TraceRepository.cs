using Serilog;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceRepository : ITraceRepository
  {
    List<Trace> _traces = new();

    public void Add(Trace trace)
    {
      _traces.Add(trace);
      Log.Information("{@CorrelationId}: {@CallingModuleName} --> {@CalledModuleName} : {@MessageName}",
        trace.CorrelationId,
        trace.CallingModuleName,
        trace.CalledModuleName,
        trace.CallName);
    }
  }
}