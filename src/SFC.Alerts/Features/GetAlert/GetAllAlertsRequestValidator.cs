using FluentValidation;

namespace SFC.Alerts.Features.GetAlert
{
  class GetAlertRequestValidator : AbstractValidator<GetAlertRequest>
  {
    public GetAlertRequestValidator()
    {
      RuleFor(x => x.LoginName).NotEmpty();
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
