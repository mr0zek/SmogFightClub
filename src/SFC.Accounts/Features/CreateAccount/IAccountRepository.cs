using SFC.SharedKernel;
using System.Threading.Tasks;

namespace SFC.Accounts.Features.CreateAccount
{
  internal interface IAccountRepository
  {
    Task Add(LoginName commandLoginName, PasswordHash passwordHash);
  }
}