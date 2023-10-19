using Serilog;
using SFC.Infrastructure.Interfaces.Tracing;
using System.Collections.Generic;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceRepository : IRequestLifecycle
  {
    public void AddModuleCall(ModuleCall trace)
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