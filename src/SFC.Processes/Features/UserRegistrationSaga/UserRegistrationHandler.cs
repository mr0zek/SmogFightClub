using Automatonymous;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Processes.Features.UserRegistrationSaga.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommandSaga>
  {
    private readonly ICommandBus _commandBus;
    private readonly ISagaRepository _sagaRepository;
    private readonly IQuery _query;

    public UserRegistrationHandler(ICommandBus commandBus, ISagaRepository sagaRepository, IQuery query)
    {
      _commandBus = commandBus;
      _sagaRepository = sagaRepository;
      _query = query;
    }

    public async Task Handle(RegisterUserCommandSaga command, CancellationToken cancellationToken)
    {
      if ((await _query.Send(new GetAccountByLoginNameRequest(command.LoginName))) != null)
      {
        throw new LoginNameAlreadyUsedSagaException(command.LoginName);
      }

      if ((await _sagaRepository.Get<UserRegistrationSagaData>(command.LoginName)) != null)
      {
        throw new LoginNameAlreadyUsedSagaException(command.LoginName);
      }

      UserRegistrationSaga saga = new(_commandBus);
      UserRegistrationSagaData data = new() { Id = command.Id };
      await saga.RaiseEvent(data, saga.RegisterUserCommand, command);
      await _sagaRepository.Save(data.Id, data);
    }
  }
}