using SFC.SharedKernel;

namespace SFC.Notifications
{
  internal interface IEmailReadRepository
  {
    Email GetEmail(LoginName loginName);
  }
}