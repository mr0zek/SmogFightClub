using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.CreateAlert
{
  internal class RegisterAlertHandler : ICommandHandler<CreateAlertCommand>
  {
    private readonly IEventBus _eventBus;
    private readonly IAlertRepository _repository;

    public RegisterAlertHandler(IEventBus eventBus, IAlertRepository repository)
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

      _repository.Add(command.ZipCode, command.LoginName);

      _eventBus.Publish(new AlertCreatedEvent()
      {
        ZipCode = command.ZipCode,
        LoginName = command.LoginName
      });
    }
  }
}
