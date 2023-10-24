using Microsoft.Extensions.DependencyInjection;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.Infrastructure.Features.Tracing
{
  [ModuleDefinition("Tracing")]
  public static class TracingConfiguration
  {

    public static IMvcBuilder AddTracing(this IMvcBuilder builder)
    {
      builder.AddMvcOptions(opt => opt.Filters.Add(typeof(TraceActionFilter)));
      return builder;
    }
  }
}
