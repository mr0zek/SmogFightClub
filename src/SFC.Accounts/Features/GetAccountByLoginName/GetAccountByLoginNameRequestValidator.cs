using FluentValidation;

namespace SFC.Accounts.Features.GetAccountByLoginName
{
  class GetAccountByLoginNameRequestValidator : AbstractValidator<GetAccountByLoginNameRequest>
  {
    public GetAccountByLoginNameRequestValidator()
    {
      RuleFor(x => x.LoginName).NotEmpty();
    }
  }
}
