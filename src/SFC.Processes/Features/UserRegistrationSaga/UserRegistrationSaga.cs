using System;
using System.Linq;
using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistrationSaga.Contract;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  public class UserRegistrationSaga : AutomatonymousStateMachine<UserRegistrationSagaData>
  {
    private readonly ICommandBus _commandBus;
    public Event<ConfirmUserCommandSaga>? ConfirmUserCommand { get; set; }
    public Event<RegisterUserCommandSaga>? RegisterUserCommand { get; set; }
    public State? WaitingForConfirmation { get; set; }

    public UserRegistrationSaga(ICommandBus commandBus)
    {
      _commandBus = commandBus;
      UserRegistrationSagaData.States = States.ToDictionary(f => f.Name, f => f);

      Initially(
        When(RegisterUserCommand)
          .Then(CopyDataToSaga)
          .Then(SaveNotificationEmail)
          .Then(SendRegistrationNotification)
          .TransitionTo(WaitingForConfirmation));

      During(WaitingForConfirmation,
        When(ConfirmUserCommand)
          .Then(CreateUserAccount)
          .Then(RegisterAlert)
          .TransitionTo(Final));
    }

    private void CopyDataToSaga(BehaviorContext<UserRegistrationSagaData, RegisterUserCommandSaga> context)
    {
      context.Instance.BaseUrl = context.Data.BaseUrl;
      context.Instance.LoginName = (context.Data.LoginName).ThrowIfNull();
      context.Instance.PasswordHash = (context.Data.PasswordHash).ThrowIfNull().Value;
      context.Instance.Email = (context.Data.Email).ThrowIfNull();
      context.Instance.ZipCode = (context.Data.ZipCode).ThrowIfNull();
    }

    private void CreateUserAccount(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAccountCommand((context.Instance?.LoginName).ThrowIfNull(), new PasswordHash((context.Instance?.PasswordHash).ThrowIfNull()))).Wait();
    }

    private void RegisterAlert(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAlertCommand(
        (context.Instance?.LoginName).ThrowIfNull(),
        (context.Instance?.ZipCode).ThrowIfNull(),
        Guid.NewGuid())).Wait();      
    }

    private void SaveNotificationEmail(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new SetNotificationEmailCommand(
        (context.Instance.Email).ThrowIfNull(),
        (context.Instance.LoginName).ThrowIfNull()
      )).Wait();
    }

    private void SendRegistrationNotification(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new SendNotificationCommand(
        (context.Instance.LoginName).ThrowIfNull(),
        $"<a href=\"{context.Instance.BaseUrl}/Confirmation/{context.Instance.Id}\">Click her to confirm</a>",
        "Registration confirmation",
        "RegistrationConfirmation")).Wait();
    }
  }
}
