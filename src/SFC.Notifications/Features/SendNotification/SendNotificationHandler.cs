using SFC.Infrastructure;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  internal class SendNotificationHandler : ICommandHandler<SendNotificationCommand>
  {
    private readonly IEmailReadRepository _emailRepository;
    private readonly ISmtpClient _smtpClient;
    private readonly INotificationRepository _notificationRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SendNotificationHandler(
      IEmailReadRepository emailRepository, 
      INotificationRepository notificationRepository, 
      ISmtpClient smtpClient, IDateTimeProvider dateTimeProvider)
    {
      _emailRepository = emailRepository;
      _notificationRepository = notificationRepository;
      _smtpClient = smtpClient;
      _dateTimeProvider = dateTimeProvider;
    }

    public void Handle(SendNotificationCommand command)
    {
      Email email = _emailRepository.GetEmail(command.LoginName);

      _smtpClient.Send(email, command.Title, command.Body);

      _notificationRepository.Add(email, command.Title, command.Body, _dateTimeProvider.Now(), command.LoginName);
    }


  }
}