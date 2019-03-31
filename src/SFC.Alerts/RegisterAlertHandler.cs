using System.Collections.Generic;
using System.Text;
using SFC.Alerts.Contract;
using SFC.Alerts.Contract.Command;
using SFC.Alerts.Contract.Event;
using SFC.Infrastructure;

namespace SFC.Alerts
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
