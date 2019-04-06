using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration.Contract
{
  public class RegisterUserCommand
  {
    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public string PasswordHash { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
    public string Id { get; set; }
  }
}