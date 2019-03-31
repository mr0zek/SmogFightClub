using Automatonymous;
using SFC.Infrastructure;
using SFC.Processes.Contract.Command;
using SFC.Users.Contract.Query;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;
    private readonly IUsersPerspective _usersPerspective;

    public UserRegistrationHandler(ICommandBus commandBus, ISagaRepository sagaRepository, IUsersPerspective usersPerspective)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
      _usersPerspective = usersPerspective;
    }

    public void Handle(RegisterUserCommand command)
    {
      if (_usersPerspective.Get(command.LoginName) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      if (_sagaRepository.Get<UserRegistrationSagaData>(command.LoginName) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      UserRegistrationSaga saga = new UserRegistrationSaga(_commandBus);
      UserRegistrationSagaData data = new UserRegistrationSagaData();
      saga.RaiseEvent(data, saga.CreateUser, command);
      _sagaRepository.Save(data.Id, data);
    }
  }
}