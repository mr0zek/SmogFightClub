using System;
using Automatonymous;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.Processes.Features.UserRegistration
{
  public class ConfirmUserHandler : ICommandHandler<ConfirmUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ConfirmUserHandler(ICommandBus commandBus, ISagaRepository sagaRepository, IPasswordHasher passwordHasher)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
      _passwordHasher = passwordHasher;
    }

    public void Handle(ConfirmUserCommand command)
    {
      UserRegistrationSaga saga = new UserRegistrationSaga(_commandBus, _passwordHasher);
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