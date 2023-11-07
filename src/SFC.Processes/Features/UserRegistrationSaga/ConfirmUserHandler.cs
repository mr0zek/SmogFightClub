using System;
using System.Threading;
using System.Threading.Tasks;
using Automatonymous;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
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

    public async Task Handle(ConfirmUserCommandSaga command, CancellationToken cancellationToken)
    {
      UserRegistrationSaga saga = new(_commandBus);
      UserRegistrationSagaData? data = await _sagaRepository.Get<UserRegistrationSagaData>(command.ConfirmationId);
      if (data == null)
      {
        throw new InvalidOperationException();
      }
      await saga.RaiseEvent(data, saga.ConfirmUserCommand, command);
      await _sagaRepository.Save(command.ConfirmationId, data);
    }
  }
}