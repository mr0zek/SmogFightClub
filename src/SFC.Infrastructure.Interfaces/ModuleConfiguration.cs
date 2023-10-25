using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SFC.Infrastructure.Interfaces
{
  public static class ModuleConfiguration
  {
    public static void AddModule(this WebApplication app, IModule module)
    {
      module.ConfigureWebApplication(app);
    }

    public static void AddModule(this IMvcBuilder builder, IModule module)
    {
      module.ConfigureMvc(builder);
      builder.AddApplicationPart(module.GetType().Assembly);
    }

  }
}
