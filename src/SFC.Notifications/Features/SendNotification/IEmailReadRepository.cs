using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.Notifications.Features.SendNotification
{
  internal interface IEmailReadRepository
  {
    Task<Email?> GetEmail(LoginName loginName);
  }
}