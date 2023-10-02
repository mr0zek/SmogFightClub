using FluentValidation;
using SFC.Notifications.Features.GetAllSendNotificationsByUser.Contract;

namespace SFC.Notifications.Features.GetAllSendNotificationsByUser
{
  internal class GetAllSendNotificationsByUserRequestValidator : AbstractValidator<GetAllSendNotificationsByUserRequest>
  {
    public GetAllSendNotificationsByUserRequestValidator()
    {
      RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
      RuleFor(x => x.Take).GreaterThan(0);
    }
  }
}