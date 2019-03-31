namespace SFC.Processes.Contract.Command
{
  public class RegisterUserCommand
  {
    public string LoginName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ZipCode { get; set; }
    public string BaseUrl { get; set; }
  }
}