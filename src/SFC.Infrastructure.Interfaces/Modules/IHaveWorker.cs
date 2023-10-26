using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.Modules
{
  public interface IHaveWorker
  {
    void StartWorker(IComponentContext container);
    void StopWorker();
    void WaitForShutdown();
  }
}
