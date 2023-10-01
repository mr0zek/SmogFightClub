using FluentValidation;

namespace SFC.Accounts.Features.CreateAccount.Contract
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
