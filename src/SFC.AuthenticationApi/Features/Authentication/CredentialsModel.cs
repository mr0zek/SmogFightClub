using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.AuthenticationApi.Features.Authentication
{
    public class CredentialsModel : ICommand
    {
        public string LoginName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
