using FluentValidation;

namespace SFC.UserApi.Features.Alerts
{
  public class PostAlertModelValidator : AbstractValidator<PostAlertModel>
  {
    public PostAlertModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}