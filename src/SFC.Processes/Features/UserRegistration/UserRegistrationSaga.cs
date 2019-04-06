using System.Linq;
using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistration.Contract;

namespace SFC.Processes.Features.UserRegistration
{
  public class UserRegistrationSaga : AutomatonymousStateMachine<UserRegistrationSagaData>
  {
    private readonly ICommandBus _commandBus;
    public Event<ConfirmUserCommand> ConfirmUserCommand { get; set; }
    public Event<RegisterUserCommand> RegisterUserCommand { get; set; }
    public State WaitingForConfirmation { get; set; }

    public UserRegistrationSaga(ICommandBus commandBus)
    {
      _commandBus = commandBus;
      UserRegistrationSagaData.States = States.ToDictionary(f=>f.Name,f=>f);

      Initially(
        When(RegisterUserCommand)
          .Then(context =>
          {
            context.Instance.BaseUrl = context.Data.BaseUrl;
            context.Instance.LoginName = context.Data.LoginName;
            context.Instance.PasswordHash = context.Data.PasswordHash;
            context.Instance.Email = context.Data.Email;
            context.Instance.ZipCode = context.Data.ZipCode;
          })
          .Then(SaveNotificationEmail)
          .Then(SendRegistrationNotification)
          .TransitionTo(WaitingForConfirmation));

      During(WaitingForConfirmation,
        When(ConfirmUserCommand)
          .Then(CreateUserAccount)
          .Then(RegisterAlertCondition)
          .TransitionTo(Final));
    }

    private void CreateUserAccount(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAccountCommand()
      {
        LoginName = context.Instance.LoginName
      });
    }

    private void RegisterAlertCondition(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new RegisterAlertConditionCommand()
      {
        LoginName = context.Instance.LoginName,
        ZipCode = context.Instance.ZipCode
      });
    }

    private void SaveNotificationEmail(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new SetNotificationEmailCommand()
      {
        Email = context.Instance.Email,
        LoginName = context.Instance.LoginName
      });
    }

    private void SendRegistrationNotification(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        LoginName = context.Instance.LoginName,
        Body = $"<a href=\"{context.Instance.BaseUrl}/Confirmation/{context.Instance.Id}\">Click her to confirm</a>",
        Title = "Registration confirmation",
        NotificationType = "RegistrationConfirmation"
      });
    }
  }
}
