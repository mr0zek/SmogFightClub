using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;

namespace SFC.Alerts.Features.RegisterAlertCondition
{
  class RegisterAlertConditionHandler : ICommandHandler<RegisterAlertConditionCommand>
  {
    private readonly IEventBus _eventBus;
    private readonly IAlertConditionsRepository _repository;

    public RegisterAlertConditionHandler(IEventBus eventBus, IAlertConditionsRepository repository)
    {
      _eventBus = eventBus;
      _repository = repository;
    }

    public void Handle(RegisterAlertConditionCommand command)
    {
      if (_repository.Exists(command.ZipCode, command.LoginName))
      {
        throw new AlertExistsException(command.ZipCode);
      }

      _repository.Add(command.ZipCode, command.LoginName);

      _eventBus.Publish(new AlertConditionRegisteredEvent()
      {
        ZipCode = command.ZipCode,
        LoginName = command.LoginName
      });
    }
  }
}
