using Automatonymous;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IQuery _query;

    public UserRegistrationHandler(ICommandBus commandBus, ISagaRepository sagaRepository, IPasswordHasher passwordHasher, IQuery query)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
      _passwordHasher = passwordHasher;
      _query = query;
    }

    public void Handle(RegisterUserCommand command)
    {
      if (_query.Query(new GetAccountByLoginNameRequest(command.LoginName)) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      if (_sagaRepository.Get<UserRegistrationSagaData>(command.LoginName) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }

      UserRegistrationSaga saga = new(_commandBus, _passwordHasher);
      UserRegistrationSagaData data = new() { Id = command.Id };
      saga.RaiseEvent(data, saga.RegisterUserCommand, command);
      _sagaRepository.Save(data.Id, data);
    }
  }
}