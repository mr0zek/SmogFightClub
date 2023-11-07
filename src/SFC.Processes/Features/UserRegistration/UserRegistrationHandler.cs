using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Alerts.Features.CreateAlert.Contract;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistration.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
      if (await _query.Send(new GetAccountByLoginNameRequest(command.LoginName)) != null)
      {
        throw new LoginNameAlreadyUsedException(command.LoginName);
      }
      
      await _accountRepository.Add(new Account(command.Id, command.Email, command.LoginName, command.ZipCode, command.PasswordHash.Value));

      await _commandBus.Send(new SetNotificationEmailCommand(
        command.Email,
        command.LoginName
      ));

      await _commandBus.Send(new SendNotificationCommand(
        command.LoginName,
        $"<a href=\"{command.BaseUrl}/Confirmation/{command.Id}\">Click her to confirm</a>",
        "Registration confirmation",
        "RegistrationConfirmation"));      
    }

    public async Task Handle(ConfirmUserCommand command, CancellationToken cancellationToken)
    {
      Account? account = await _accountRepository.Get(command.ConfirmationId);
      if(account == null)
      {
        throw new InvalidOperationException();
      }

      await _commandBus.Send(new CreateAccountCommand(account.LoginName, new SharedKernel.PasswordHash(account.PasswordHash)));

      await _commandBus.Send(new CreateAlertCommand(account.LoginName, account.ZipCode, Guid.NewGuid()));
    }
  }
}