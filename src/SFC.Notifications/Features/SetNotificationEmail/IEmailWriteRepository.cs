using SFC.SharedKernel;

namespace SFC.Notifications.Features.SetNotificationEmail
{
  internal interface IEmailWriteRepository
  {
    void Set(LoginName loginName, Email email);
  }
}
