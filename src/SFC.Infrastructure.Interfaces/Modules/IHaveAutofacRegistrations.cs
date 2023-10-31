using Autofac;

namespace SFC.Infrastructure.Interfaces.Modules
{
  public interface IHaveAutofacRegistrations
  {
    void RegisterTypes(ContainerBuilder builder);
  }
}
