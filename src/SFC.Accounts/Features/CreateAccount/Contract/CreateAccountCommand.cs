using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.CreateAccount.Contract
{
  public class CreateAccountCommand : ICommand
  {
    public LoginName LoginName { get; set; }
    public string PasswordHash { get; set; }
  }
}
