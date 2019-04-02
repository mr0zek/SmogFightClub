using Automatonymous;
using SFC.Alerts.Contract.Command;
using SFC.Infrastructure;
using SFC.Notifications.Contract;
using SFC.Processes.Contract.Command;
using SFC.Users.Contract;
using SFC.Users.Contract.Command;

namespace SFC.Processes.Features.UserRegistration
{
  public class UserRegistrationSaga : AutomatonymousStateMachine<UserRegistrationSagaData>
  {
    private readonly ICommandBus _commandBus;
    public Event<ConfirmUserCommand> UserConfirmation { get; set; }
    public Event<RegisterUserCommand> CreateUser { get; set; }
    public State WaitingForConfirmation { get; set; }

    public UserRegistrationSaga(ICommandBus commandBus)
    {
      _commandBus = commandBus;

      Initially(
        When(CreateUser)
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
        When(UserConfirmation)
          .Then(CreateUserAccount)
          .Then(RegisterAlert)
          .TransitionTo(Final));
    }

    private void CreateUserAccount(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAccount()
      {
        LoginName = context.Instance.LoginName
      });
    }

    private void RegisterAlert(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new RegisterAlertCommand()
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
        Title = "Registration confirmation"
      });
    }
  }
}
