using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.SetNotificationEmail.Contract
{
  public class SetNotificationEmailCommand : ICommand
  {
    public Email Email { get; set; }
    public LoginName LoginName { get; set; }
  }
}