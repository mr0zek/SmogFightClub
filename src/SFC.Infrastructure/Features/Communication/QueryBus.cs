using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using MediatR;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Infrastructure.Features.Communication
{
  class QueryBus : Mediator, IQuery
  {
    public QueryBus(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}
