using SFC.SharedKernel;

namespace SFC.Notifications
{
  internal interface IEmailRepository
  {
    void Set(LoginName loginName, Email email);
  }
}
