using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.SetNotificationEmail
{
  internal class SetNotificationEmailHandler : ICommandHandler<SetNotificationEmailCommand>
  {
    private readonly IEmailWriteRepository _emailRepository;

    public SetNotificationEmailHandler(IEmailWriteRepository emailRepository)
    {
      _emailRepository = emailRepository;
    }

    public async Task Handle(SetNotificationEmailCommand command, CancellationToken cancellationToken)
    {
      await _emailRepository.Set(command.LoginName, command.Email);
    }

    
  }
}