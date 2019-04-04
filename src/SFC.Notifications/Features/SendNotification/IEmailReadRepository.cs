using SFC.SharedKernel;

namespace SFC.Notifications.Features.SendNotification
{
  internal interface IEmailReadRepository
  {
    Email GetEmail(LoginName loginName);
  }
}