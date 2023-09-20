using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Fake
{
  public class FakeIdentityProvider : IIdentityProvider
  {
    public LoginName GetLoginName()
    {
      return "Bugs Bunny";
    }
  }
}