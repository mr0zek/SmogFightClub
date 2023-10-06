namespace SFC.AuthenticationApi.Authentication
{
  public interface ITokenRepository
  {
    string Authenticate(CredentialsModel users);
  }
}
