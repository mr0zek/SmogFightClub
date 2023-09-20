using SFC.SharedKernel;

namespace SFC.Infrastructure.Interfaces
{
  public interface IIdentityProvider
  {
    LoginName GetLoginName();
  }
}