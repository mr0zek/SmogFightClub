using Microsoft.Extensions.DependencyInjection;
using SFC.Infrastructure.Interfaces.Documentation;

namespace SFC.Infrastructure.Features.Validation
{
  [ModuleDefinition("Validation")]
  public static class ValidationConfiguration
  {

    public static IMvcBuilder AddValidation(this IMvcBuilder builder)
    {
      builder.AddMvcOptions(opt => opt.Filters.Add(typeof(FluentValidationActionFilter)));
      return builder;
    }
  }
}
