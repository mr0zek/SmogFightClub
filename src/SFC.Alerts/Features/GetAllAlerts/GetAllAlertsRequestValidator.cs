using FluentValidation;

namespace SFC.Alerts.Features.GetAllAlerts
{
    class GetAllAlertsRequestValidator : AbstractValidator<GetAllAlertsRequest>
    {
        public GetAllAlertsRequestValidator()
        {
            RuleFor(x => x.LoginName).NotEmpty();
        }
    }
}
