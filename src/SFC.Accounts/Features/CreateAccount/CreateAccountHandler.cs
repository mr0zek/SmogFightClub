using System;
using System.Collections.Generic;
using System.Text;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Infrastructure.Interfaces;

namespace SFC.Accounts.Features.CreateAccount
{
  class CreateAccountHandler : ICommandHandler<CreateAccountCommand>
  {
    private readonly IAccountRepository _accountRepository;

    public CreateAccountHandler(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public void Handle(CreateAccountCommand command)
    {
      _accountRepository.Add(command.LoginName);
    }
  }
}
