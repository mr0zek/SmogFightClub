using SFC.SharedKernel;

namespace SFC.Accounts.Features.Authenticate
{
  internal interface IAuthenticationRepository
  {
    bool Authenticate(LoginName loginName, PasswordHash hash);
  }
}