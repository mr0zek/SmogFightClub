using SFC.SharedKernel;

namespace SFC.Accounts.Features.CreateAccount
{
  internal interface IAccountRepository
  {
    void Add(LoginName commandLoginName, string passwordHash);
  }
}