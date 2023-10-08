using System;
using Automatonymous;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistrationSaga.Contract;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  public class ConfirmUserHandler : ICommandHandler<ConfirmUserCommandSaga>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;

    public ConfirmUserHandler(ICommandBus commandBus, ISagaRepository sagaRepository)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
    }

    public void Handle(ConfirmUserCommandSaga command)
    {
      UserRegistrationSaga saga = new(_commandBus);
      UserRegistrationSagaData data = _sagaRepository.Get<UserRegistrationSagaData>(command.ConfirmationId);
      if (data == null)
      {
        throw new InvalidOperationException();
      }
      saga.RaiseEvent(data, saga.ConfirmUserCommand, command);
      _sagaRepository.Save(command.ConfirmationId, data);
    }
  }
}