using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SetNotificationEmail.Contract
{
  public class SetNotificationEmailCommand : ICommand
  {
    public SetNotificationEmailCommand(string email, LoginName loginName)
    {
      Email = email;
      LoginName = loginName;
    }

    public Email Email { get; set; }
    public LoginName LoginName { get; set; }
  }
}