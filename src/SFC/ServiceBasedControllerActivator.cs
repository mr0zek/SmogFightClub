using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace SFC
{
  public class ServiceBasedControllerActivator : IControllerActivator
  {
    public object Create(ControllerContext actionContext)
    {
      var controllerType = actionContext.ActionDescriptor.ControllerTypeInfo.AsType();

      return actionContext.HttpContext.RequestServices.GetRequiredService(controllerType);
    }

    public virtual void Release(ControllerContext context, object controller)
    {
    }
  }
}
