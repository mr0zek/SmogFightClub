using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using SFC.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using JJMasterData.Web.Extensions;
using JJMasterData.Web;

namespace SFC.AdminUI
{
  public class AddminUIModule : Module, IModule
  {
    public void ConfigureMvc(IMvcBuilder builder)
    {
      builder.Services.AddJJMasterDataWeb();
    }

    public void ConfigureWebApplication(WebApplication app)
    {
      app.UseJJMasterDataWeb();
    }
  }
}
