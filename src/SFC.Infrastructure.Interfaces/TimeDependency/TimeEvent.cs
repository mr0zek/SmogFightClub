﻿using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure.Interfaces.TimeDependency
{
  public class TimeEvent : IEvent
  {
    public TimeEvent(DateTime dateTime)
    {
      DateTime = dateTime;
    }

    public DateTime DateTime { get; }
  }
}
