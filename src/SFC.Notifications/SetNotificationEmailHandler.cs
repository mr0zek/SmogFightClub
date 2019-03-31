using SFC.Infrastructure;
using SFC.Notifications.Contract;

namespace SFC.Notifications
{
  internal class SetNotificationEmailHandler : ICommandHandler<SetNotificationEmailCommand>
  {
    private readonly IEmailRepository _emailRepository;

    public SetNotificationEmailHandler(IEmailRepository emailRepository)
    {
      _emailRepository = emailRepository;
    }

    public void Handle(SetNotificationEmailCommand command)
    {
      _emailRepository.Set(command.LoginName, command.Email);
    }

    
  }
}