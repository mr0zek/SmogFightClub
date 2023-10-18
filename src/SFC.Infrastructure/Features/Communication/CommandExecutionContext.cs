﻿using System;

namespace SFC.Infrastructure.Interfaces.Communication
{
  class CommandExecutionContext<T> : ICommandExecutionContext<T>
    where T : ICommand
  {
    public T Command { get; set; }

    public Exception Exception { get; set; }

    public ICommandHandler<T> Handler
    {
      get; set;
    }
  }
}