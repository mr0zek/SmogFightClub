﻿namespace SFC.Infrastructure.Interfaces.Communication
{
  public interface ICommandHandler<T>
    where T : ICommand
  {
    void Handle(T command);
  }
}