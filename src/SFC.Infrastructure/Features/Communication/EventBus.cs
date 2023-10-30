using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using MediatR;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;
using static System.Collections.Specialized.BitVector32;

namespace SFC.Infrastructure.Features.Communication
{
  class EventBus : Mediator, IEventBus
  {
    public EventBus(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}
