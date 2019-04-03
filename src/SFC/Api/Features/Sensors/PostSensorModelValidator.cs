using FluentValidation;

namespace SFC.Api.Features.Sensors
{
  public class PostSensorModelValidator : AbstractValidator<PostSensorModel>
  {
    public PostSensorModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}