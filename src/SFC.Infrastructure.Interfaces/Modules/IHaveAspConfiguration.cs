using Microsoft.AspNetCore.Builder;

namespace SFC.Infrastructure.Interfaces.Modules
{
  public interface IHaveAspConfiguration
  {    
    void Configure(WebApplicationBuilder builder);
    void Configure(WebApplication app);
  }
}
