using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.UserApi.Features.Accounts
{
  public class PostAccountModel : ICommand
  {
    public string LoginName { get; set; }
    public string ZipCode { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}