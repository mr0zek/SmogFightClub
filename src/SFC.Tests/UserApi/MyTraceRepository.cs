using SFC.Infrastructure.Features.Tracing;
using SFC.Infrastructure.Interfaces.Tracing;
using System.Collections.Generic;
using System.IO;

namespace SFC.Tests.UserApi
{
  internal class MyTraceRepository : IRequestLifecycle
  {
    private readonly string _outputPath;

    public static Dictionary<string, List<ModuleCall>> Traces { get; } = new Dictionary<string, List<ModuleCall>>();

    public MyTraceRepository(string outputPath)
    {
      _outputPath = outputPath;
    }

    public void AddModuleCall(ModuleCall trace)
    {
      Traces[trace.CorrelationId].Add(trace);
    }

      
    public void BeginRequest(string correlationId)
    {
      Traces.Add(correlationId, new List<ModuleCall>());
    }


    public void EndRequest(string correlationId)
    {
      
    }
  }
}