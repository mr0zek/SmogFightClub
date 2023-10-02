using FluentValidation;
using SFC.Notifications.Features.GetAllSendNotificationsCount.Contract;

namespace SFC.Notifications.Features.GetAllSendNotificationsCount
{
  internal class GetAllSendNotificationsCountRequestValidator : AbstractValidator<GetAllSendNotificationsCountRequest>
  {
    public GetAllSendNotificationsCountRequestValidator()
    {
      RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
      RuleFor(x => x.Take).GreaterThan(0);
    }
  }
}