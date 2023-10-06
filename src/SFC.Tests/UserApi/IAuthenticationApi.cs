using RestEase;
using System.Threading.Tasks;

namespace SFC.Tests.UserApi
{
  public interface IAuthenticationApi
  {
    [Post("api/v1.0/authentication")]
    Task<string> Login([Body] CredentialsModel model);
  }
}