using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.SetNotificationEmail
{
  internal interface IEmailWriteRepository
  {
    Task Set(LoginName loginName, Email email);
  }
}
