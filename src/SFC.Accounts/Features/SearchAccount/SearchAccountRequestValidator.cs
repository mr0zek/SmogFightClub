using FluentValidation;

namespace SFC.Accounts.Features.SearchAccount
{
  class SearchAccountRequestValidator : AbstractValidator<SearchAccountRequest>
  {
    public SearchAccountRequestValidator()
    {
      RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
      RuleFor(x=> x.Take).GreaterThan(0);
    }
  }
}