using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Fake
{
  public class FakeIdentityProvider : IIdentityProvider
  {
    private static LoginName _loginName = "Bugs Bunny";

    public void SetLoginName(string loginName)
    {
      _loginName = loginName;
    }
    public LoginName GetLoginName()
    {
      return _loginName;
    }
  }
}