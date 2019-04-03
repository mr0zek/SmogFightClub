using FluentValidation;

namespace SFC.Api.Features.Alerts
{
  public class PostAlertModelValidator : AbstractValidator<PostAlertModel>
  {
    public PostAlertModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}