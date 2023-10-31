using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Accounts.Features.CreateAccount
{
  class CreateAccountHandler : ICommandHandler<CreateAccountCommand>
  {
    private readonly IAccountRepository _accountRepository;
    private readonly IEventBus _eventBus;

    public CreateAccountHandler(IAccountRepository accountRepository, IEventBus eventBus)
    {
      _accountRepository = accountRepository;
      _eventBus = eventBus;
    }

    public async Task Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
      await _accountRepository.Add(command.LoginName, command.PasswordHash);

      await _eventBus.Publish(new AccountCreatedEvent(command.LoginName), cancellationToken);
    }
  }
}
