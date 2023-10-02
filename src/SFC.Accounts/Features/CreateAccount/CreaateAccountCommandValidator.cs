using FluentValidation;
using SFC.Accounts.Features.CreateAccount.Contract;

namespace SFC.Accounts.Features.CreateAccount
{
  class CreaateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
  {
    public CreaateAccountCommandValidator()
    {
      RuleFor(f => f.LoginName).NotEmpty();
      RuleFor(f => f.PasswordHash).NotEmpty();
    }
  }
}
