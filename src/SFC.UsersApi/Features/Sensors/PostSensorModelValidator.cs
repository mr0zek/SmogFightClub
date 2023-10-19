using FluentValidation;

namespace SFC.UserApi.Features.Sensors
{
  class PostSensorModelValidator : AbstractValidator<PostSensorModel>
  {
    public PostSensorModelValidator()
    {
      RuleFor(f => f.ZipCode).NotEmpty();
    }
  }
}