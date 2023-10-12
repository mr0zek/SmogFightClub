using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Alerts.Features.VerifySmogExceedence;
using SFC.Alerts.Features.VerifySmogExceedence.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.CreateAlert
{
  internal class CreateAlertHandler : ICommandHandler<CreateAlertCommand>
  {
    private readonly IEventBus _eventBus;
    private readonly IAlertWriteRepository _repository;

    public CreateAlertHandler(IEventBus eventBus, IAlertWriteRepository repository)
    {
      _eventBus = eventBus;
      _repository = repository;
    }

    public void Handle(CreateAlertCommand command)
    {
      if (_repository.Exists(command.ZipCode, command.LoginName))
      {
        throw new AlertExistsException(command.ZipCode);
      }

      _repository.Add(command.Id, command.ZipCode, command.LoginName);

      _eventBus.Publish(new AlertCreatedEvent()
      {
        ZipCode = command.ZipCode,
        LoginName = command.LoginName
      });
    }
  }
}
