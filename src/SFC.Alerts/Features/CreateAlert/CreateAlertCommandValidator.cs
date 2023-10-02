using FluentValidation;
using SFC.Alerts.Features.CreateAlert.Contract;

namespace SFC.Alerts.Features.CreateAlert
{
    class CreateAlertCommandValidator : AbstractValidator<CreateAlertCommand>
    {
        public CreateAlertCommandValidator()
        {
            RuleFor(x => x.LoginName).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}