using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace SFC.Infrastructure.Interfaces
{
  public interface IModule
  {
    void ConfigureMvc(IMvcBuilder builder);
    void ConfigureWebApplication(WebApplication app);
  }
}
