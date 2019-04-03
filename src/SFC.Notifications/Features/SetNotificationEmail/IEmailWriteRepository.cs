using SFC.SharedKernel;

namespace SFC.Notifications
{
  internal interface IEmailWriteRepository
  {
    void Set(LoginName loginName, Email email);
  }
}
