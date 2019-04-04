using SFC.SharedKernel;

namespace SFC.Notifications.Features.SetNotificationEmail.Contract
{
  public class SetNotificationEmailCommand
  {
    public Email Email { get; set; }
    public LoginName LoginName { get; set; }
  }
}