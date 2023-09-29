using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistration.Contract;
using System;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>, ICommandHandler<ConfirmUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IQuery _query;
    private readonly IAccountRepository _accountRepository;

    public UserRegistrationHandler(ICommandBus commandBus, IPasswordHasher passwordHasher, IQuery query, IAccountRepository accountRepository)
    {
      _commandBus = commandBus;
      _passwordHasher = passwordHasher;
      _query = query;
      _accountRepository = accountRepository;
    }

    public void Handle(RegisterUserCommand command)
    {
      if (_query.Query(new GetAccountByLoginNameRequest(command.LoginName)) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }
      
      var passwordHash = _passwordHasher.Hash(command.Password);

      _accountRepository.Add(new Account(command.Id, command.Email, command.LoginName, command.ZipCode, passwordHash));

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

      _commandBus.Send(new CreateAccountCommand()
      {
        LoginName = account.LoginName,
        PasswordHash = account.PasswordHash
      });      

      _commandBus.Send(new CreateAlertCommand()
      {
        LoginName = account.LoginName,
        ZipCode = account.ZipCode
      });
    }
  }
}