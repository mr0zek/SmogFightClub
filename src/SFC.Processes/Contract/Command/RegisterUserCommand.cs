using SFC.SharedKernel;

namespace SFC.Processes.Contract.Command
{
  public class RegisterUserCommand
  {
    public LoginName LoginName { get; set; }
    public Email Email { get; set; }
    public string PasswordHash { get; set; }
    public ZipCode ZipCode { get; set; }
    public string BaseUrl { get; set; }
  }
}