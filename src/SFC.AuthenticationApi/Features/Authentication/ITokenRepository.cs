namespace SFC.AuthenticationApi.Features.Authentication
{
    public interface ITokenRepository
    {
        Task<string?> Authenticate(CredentialsModel users);
    }
}
