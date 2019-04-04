using FluentValidation;

namespace SFC.UserApi.Features.Sensors
{
  public class PostSensorModelValidator : AbstractValidator<PostSensorModel>
  {
    public PostSensorModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}