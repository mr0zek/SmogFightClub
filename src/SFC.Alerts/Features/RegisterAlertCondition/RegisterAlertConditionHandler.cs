using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.RegisterAlertCondition
{
  internal class RegisterAlertConditionHandler : ICommandHandler<CreateAlertCommand>
  {
    private readonly IEventBus _eventBus;
    private readonly IAlertRepository _repository;

    public RegisterAlertConditionHandler(IEventBus eventBus, IAlertRepository repository)
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
