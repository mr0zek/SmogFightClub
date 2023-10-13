using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistration.Contract;
using System;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>, ICommandHandler<ConfirmUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly IQuery _query;
    private readonly IAccountRepository _accountRepository;

    public UserRegistrationHandler(ICommandBus commandBus, IQuery query, IAccountRepository accountRepository)
    {
      _commandBus = commandBus;
      _query = query;
      _accountRepository = accountRepository;
    }

    public void Handle(RegisterUserCommand command)
    {
      if (_query.Query(new GetAccountByLoginNameRequest(command.LoginName)) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }
      
      _accountRepository.Add(new Account(command.Id, command.Email, command.LoginName, command.ZipCode, command.PasswordHash.Value));

      _commandBus.Send(new SetNotificationEmailCommand(
        command.Email,
        command.LoginName
      ));

      _commandBus.Send(new SendNotificationCommand()
      {
        LoginName = command.LoginName,
        Body = $"<a href=\"{command.BaseUrl}/Confirmation/{command.Id}\">Click her to confirm</a>",
        Title = "Registration confirmation",
        NotificationType = "RegistrationConfirmation"
      });
    }

    public void Handle(ConfirmUserCommand command)
    {
      Account account = _accountRepository.Get(command.ConfirmationId);
      if(account == null)
      {
        throw new InvalidOperationException();
      }

      _commandBus.Send(new CreateAccountCommand(account.LoginName, new SharedKernel.PasswordHash(account.PasswordHash)));

      _commandBus.Send(new CreateAlertCommand()
      {
        Id = Guid.NewGuid(),
        LoginName = account.LoginName,
        ZipCode = account.ZipCode
      }) ;
    }
  }
}