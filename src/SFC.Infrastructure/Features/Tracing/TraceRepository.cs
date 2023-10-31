using Serilog;
using SFC.Infrastructure.Interfaces.Tracing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Features.Tracing
{
  class TraceRepository : IRequestLifecycle
  {
    public async Task AddModuleCall(ModuleCall trace)
    {
      Log.Information("{@CorrelationId}: {@CallingModuleName} -[{@CallName}]-> {@CalledModuleName} : {@MessageName}",
        trace.CorrelationId,
        trace.CallingModuleName,
        trace.CallType[0].ToString(),        
        trace.CalledModuleName,
        trace.CallName);
    }

    public async Task BeginRequest(string correlationId)
    {
    }

    public async Task EndRequest(string correlationId)
    {
    }
  }
}