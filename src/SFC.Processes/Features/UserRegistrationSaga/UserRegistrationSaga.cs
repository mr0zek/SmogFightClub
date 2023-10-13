﻿using System;
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
    public Event<ConfirmUserCommandSaga> ConfirmUserCommand { get; set; }
    public Event<RegisterUserCommandSaga> RegisterUserCommand { get; set; }
    public State WaitingForConfirmation { get; set; }

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
      context.Instance.LoginName = context.Data.LoginName;
      context.Instance.PasswordHash = context.Data.PasswordHash.Value;
      context.Instance.Email = context.Data.Email;
      context.Instance.ZipCode = context.Data.ZipCode;
    }

    private void CreateUserAccount(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAccountCommand(context.Instance.LoginName,new PasswordHash(context.Instance.PasswordHash)));
    }

    private void RegisterAlert(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new CreateAlertCommand()
      {
        Id = Guid.NewGuid(),
        LoginName = context.Instance.LoginName,
        ZipCode = context.Instance.ZipCode
      }); 
    }

    private void SaveNotificationEmail(BehaviorContext<UserRegistrationSagaData> context)
    {
      _commandBus.Send(new SetNotificationEmailCommand(
        context.Instance.Email,
        context.Instance.LoginName
      ));
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
