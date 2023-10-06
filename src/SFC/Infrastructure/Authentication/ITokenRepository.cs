namespace SFC.Infrastructure.Authentication
{
  public interface ITokenRepository
  {
    Tokens Authenticate(Users users);
  }
}
