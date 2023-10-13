using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SetNotificationEmail.Contract;

namespace SFC.Notifications.Features.SetNotificationEmail
{
  internal class SetNotificationEmailHandler : ICommandHandler<SetNotificationEmailCommand>
  {
    private readonly IEmailWriteRepository _emailRepository;

    public SetNotificationEmailHandler(IEmailWriteRepository emailRepository)
    {
      _emailRepository = emailRepository;
    }

    public void Handle(SetNotificationEmailCommand command)
    {
      _emailRepository.Set(command.LoginName, command.Email);
    }

    
  }
}