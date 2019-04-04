using SFC.Alerts.Features.RegisterAlert.Contract;
using SFC.Infrastructure;

namespace SFC.Alerts.Features.RegisterAlert
{
  class RegisterAlertHandler : ICommandHandler<RegisterAlertCommand>
  {
    private readonly IEventBus _eventBus;
    private readonly IAlertsRepository _repository;

    public RegisterAlertHandler(IEventBus eventBus, IAlertsRepository repository)
    {
      _eventBus = eventBus;
      _repository = repository;
    }

    public void Handle(RegisterAlertCommand command)
    {
      if (_repository.Exists(command.ZipCode, command.LoginName))
      {
        throw new AlertExistsException(command.ZipCode);
      }

      _repository.Add(command.ZipCode, command.LoginName);
      
      _eventBus.Publish(new AlertRegisteredEvent()
      {
        ZipCode = command.ZipCode,
        LoginName = command.LoginName
      });
    }
  }
}
