using Automatonymous;
using SFC.Accounts.Features.AccountQuery;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;
    private readonly IAccountsPerspective _accountsPerspective;
    private readonly IPasswordHasher _passwordHasher;

    public UserRegistrationHandler(ICommandBus commandBus, ISagaRepository sagaRepository, IAccountsPerspective accountsPerspective, IPasswordHasher passwordHasher)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
      _accountsPerspective = accountsPerspective;
      _passwordHasher = passwordHasher;
    }

    public void Handle(RegisterUserCommand command)
    {
      if (_accountsPerspective.Get(command.LoginName) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      if (_sagaRepository.Get<UserRegistrationSagaData>(command.LoginName) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      UserRegistrationSaga saga = new UserRegistrationSaga(_commandBus, _passwordHasher);
      UserRegistrationSagaData data = new UserRegistrationSagaData {Id = command.Id};
      saga.RaiseEvent(data, saga.RegisterUserCommand, command);
      _sagaRepository.Save(data.Id, data);
    }
  }
}