using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.Authenticate
{
  public class AuthenticateRequest : IRequest<AuthenticateResponse>
  {
    public AuthenticateRequest(LoginName loginName, PasswordHash passwordHash)
    {
      LoginName = loginName;
      PasswordHash = passwordHash;
    }

    public LoginName LoginName { get; set; }
    public PasswordHash PasswordHash { get; }
  }
}