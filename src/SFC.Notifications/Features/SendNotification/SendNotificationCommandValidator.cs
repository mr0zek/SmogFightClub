using FluentValidation;
using SFC.Notifications.Features.SendNotification.Contract;

namespace SFC.Notifications.Features.SendNotification
{
  internal class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
  {
    public SendNotificationCommandValidator()
    {
      RuleFor(x => x.LoginName).NotEmpty();
      RuleFor(x => x.Body).NotEmpty();
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.NotificationType).NotEmpty();
    }
  }
}