using Automatonymous;
using SFC.Infrastructure;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.Processes.Features.UserRegistration
{
  public class ConfirmUserHandler : ICommandHandler<ConfirmUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;

    public ConfirmUserHandler(ICommandBus commandBus, ISagaRepository sagaRepository)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
    }

    public void Handle(ConfirmUserCommand command)
    {
      UserRegistrationSaga saga = new UserRegistrationSaga(_commandBus);
      UserRegistrationSagaData data = _sagaRepository.Get<UserRegistrationSagaData>(command.LoginName);
      saga.RaiseEvent(data, saga.ConfirmUserCommand, command);
      _sagaRepository.Save(command.LoginName, data);
    }
  }
}