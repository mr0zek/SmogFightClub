using Serilog;
using SFC.Infrastructure.Interfaces;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Processes.Features.MonthlyStatus
{
  [Crontab("* * * * *")]
  internal class MonthlyStatusHandler : IEventHandler<TimeEvent>
  {    
    public void Handle(TimeEvent @event)
    {
      
    }
  }
}
