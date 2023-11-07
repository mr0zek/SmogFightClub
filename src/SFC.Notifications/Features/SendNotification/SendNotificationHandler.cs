using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Infrastructure.Interfaces.Smtp;
using SFC.Infrastructure.Interfaces.TimeDependency;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.SharedKernel;
using System.Threading;
using System.Threading.Tasks;

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

    public async Task Handle(SendNotificationCommand command, CancellationToken cancellationToken)
    {
      Email? email = await _emailRepository.GetEmail(command.LoginName);
      if (email == null)
      {
        throw new UserNotFoundException(command.LoginName);
      }

      await _smtpClient.Send(email, command.Title, command.Body);

      await _notificationRepository.Add(email, command.Title, command.Body, _dateTimeProvider.Now(), command.LoginName, command.NotificationType);

      await _eventBus.Publish(new NotificationSentEvent(command.LoginName, email, command.NotificationType));      
    }
  }
}