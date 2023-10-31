using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using MediatR.NotificationPublishers;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Infrastructure.Features.Communication
{
  class EventBus : Mediator, IEventBus
  {
    public EventBus(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}
