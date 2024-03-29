﻿using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.TimeDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Processes.Features.MonthlyStatus
{
  [Crontab("* * * * *")]
  internal class MonthlyStatusHandler : IEventHandler<TimeEvent>
  {
    public async Task Handle(TimeEvent @event, CancellationToken cancellationToken)
    {      
    }
  }
}
