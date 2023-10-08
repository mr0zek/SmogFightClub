using SFC.Infrastructure.Interfaces;

namespace SFC.Accounts.Features.Authenticate
{
  public class AuthenticateResponse : IResponse
  {
    public AuthenticateResponse(bool success)
    {
      Success = success;
    }

    public bool Success { get; }
  }
}