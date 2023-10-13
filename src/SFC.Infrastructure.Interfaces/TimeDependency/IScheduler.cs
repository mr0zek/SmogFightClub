using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.TimeDependency
{
  public interface IScheduler
  {
    void RegisterRecurrentTasks();
  }
}
