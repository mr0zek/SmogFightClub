using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Serilog;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Infrastructure.Features.Communication
{
  class CommandBus : Mediator, ICommandBus
  {
    public CommandBus(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
  }
}
