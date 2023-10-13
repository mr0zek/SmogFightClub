using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Accounts.Features.Authenticate
{
  internal class AuthenticateHandler : IQueryHandler<AuthenticateRequest, AuthenticateResponse>
  {
    private IAuthenticationRepository _authenticationRepository;

    public AuthenticateHandler(IAuthenticationRepository authenticationRepository)
    {
      _authenticationRepository = authenticationRepository;
    }

    public AuthenticateResponse HandleQuery(AuthenticateRequest query)
    {
      return new AuthenticateResponse(_authenticationRepository.Authenticate(query.LoginName, query.PasswordHash));
    }
  }
}
