using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SFC.Infrastructure;

namespace SFC
{
  public class Bootstrap
  {
    public static void Run(string[] args, Action<ContainerBuilder> overrideDependencies = null)
    {
      if (overrideDependencies != null)
      {
        Startup.RegisterExternalTypes = overrideDependencies;
      }

      BaseUrl.Current = "http://localhost:5000";
      IWebHostBuilder whb = WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseStartup<Startup>();

      ThreadPool.QueueUserWorkItem(state => { whb.Build().Run(); });
    }
  }
}
