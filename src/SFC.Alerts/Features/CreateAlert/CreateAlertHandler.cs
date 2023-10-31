using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Alerts.Features.VerifySmogExceedence;
using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.CreateAlert
{
  internal class CreateAlertHandler : ICommandHandler<CreateAlertCommand>
  {
    private readonly IEventBusWithAsync _eventBus;
    private readonly IAlertWriteRepository _repository;

    public CreateAlertHandler(IEventBusWithAsync eventBus, IAlertWriteRepository repository)
    {
      _eventBus = eventBus;
      _repository = repository;
    }

    public async Task Handle(CreateAlertCommand command, CancellationToken cancellationToken)
    {
      if (await _repository.Exists(command.ZipCode, command.LoginName))
      {
        throw new AlertExistsException(command.ZipCode);
      }

      await _repository.Add(command.Id, command.ZipCode, command.LoginName);

      await _eventBus.Publish(new AlertCreatedEvent()
      {
        ZipCode = command.ZipCode,
        LoginName = command.LoginName
      });
    }
  }
}
