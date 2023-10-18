using SFC.Infrastructure.Features.Tracing;
using System.Collections.Generic;

namespace SFC.Tests.UserApi
{
  internal class MyTraceRepository : ITraceRepository
  {
    public static Dictionary<string, List<Trace>> Traces { get; } = new Dictionary<string, List<Trace>>();

    public void AddCall(Trace trace)
    {
      Traces[trace.CorrelationId].Add(trace);
    }


    public void BeginRequest(string correlationId)
    {
      Traces.Add(correlationId, new List<Trace>());
    }


    public void EndRequest(string correlationId)
    {
    }
  }
}