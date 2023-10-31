using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.Accounts.Features.Authenticate
{
  internal interface IAuthenticationRepository
  {
    Task<bool> Authenticate(LoginName loginName, PasswordHash hash);
  }
}