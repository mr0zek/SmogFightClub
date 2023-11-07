using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistrationSaga.Contract
{
    public class RegisterUserCommandSaga : ICommand
  {
    public RegisterUserCommandSaga(LoginName loginName, Email email, PasswordHash passwordHash, ZipCode zipCode, string baseUrl, string id)
    {
      LoginName = loginName;
      Email = email;
      PasswordHash = passwordHash;
      ZipCode = zipCode;
      BaseUrl = baseUrl;
      Id = id;
    }

    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public PasswordHash PasswordHash { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
    public string Id { get; set; }
  }
}