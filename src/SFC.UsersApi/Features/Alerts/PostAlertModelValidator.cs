using FluentValidation;

namespace SFC.UserApi.Features.Alerts
{
  class PostAlertModelValidator : AbstractValidator<PostAlertModel>
  {
    public PostAlertModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}