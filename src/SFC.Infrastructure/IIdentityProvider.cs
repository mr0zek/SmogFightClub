using SFC.SharedKernel;

namespace SFC.Infrastructure
{
  public interface IIdentityProvider
  {
    LoginName GetLoginName();
  }
}