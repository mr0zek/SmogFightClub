using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
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
    private readonly IEventBus _eventBus;

    public SendNotificationHandler(
      IEmailReadRepository emailRepository, 
      INotificationRepository notificationRepository, 
      ISmtpClient smtpClient, 
      IDateTimeProvider dateTimeProvider, 
      IEventBus eventBus)
    {
      _emailRepository = emailRepository;
      _notificationRepository = notificationRepository;
      _smtpClient = smtpClient;
      _dateTimeProvider = dateTimeProvider;
      _eventBus = eventBus;
    }

    public void Handle(SendNotificationCommand command)
    {
      Email email = _emailRepository.GetEmail(command.LoginName);

      _smtpClient.Send(email, command.Title, command.Body);

      _notificationRepository.Add(email, command.Title, command.Body, _dateTimeProvider.Now(), command.LoginName, command.NotificationType);

      _eventBus.Publish(new NotificationSentEvent()
      {
        LoginName = command.LoginName,
        Email = email,
        NotificationType = command.NotificationType
      });
    }


  }
}