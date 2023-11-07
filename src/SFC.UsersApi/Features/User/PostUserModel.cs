using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.UserApi.Features.User
{
  public class PostUserModel : ICommand
  {
    public string? Email { get; set; }
  }
}