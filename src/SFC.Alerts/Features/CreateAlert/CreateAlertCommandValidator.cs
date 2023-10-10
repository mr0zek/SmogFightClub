using FluentValidation;
using SFC.Alerts.Features.CreateAlert.Contract;

namespace SFC.Alerts.Features.CreateAlert
{
    class CreateAlertCommandValidator : AbstractValidator<CreateAlertCommand>
    {
        public CreateAlertCommandValidator()
        {
            RuleFor(x => x.LoginName).NotNull().NotEmpty();
            RuleFor(x => x.ZipCode).NotNull().NotEmpty();
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}