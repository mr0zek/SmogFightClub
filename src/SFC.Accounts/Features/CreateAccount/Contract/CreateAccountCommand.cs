using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System.Text;
using System.Security.Cryptography;

namespace SFC.Accounts.Features.CreateAccount.Contract
{
  public class CreateAccountCommand : ICommand
  {
    public LoginName LoginName { get; set; }
    public PasswordHash PasswordHash { get; set; }

    public CreateAccountCommand(LoginName loginName, PasswordHash passwordHash)
    {
      LoginName = loginName;
      PasswordHash = passwordHash;
    }
  }
}
