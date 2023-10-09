namespace SFC.AuthenticationApi.Features.Authentication
{
    public interface ITokenRepository
    {
        string? Authenticate(CredentialsModel users);
    }
}
