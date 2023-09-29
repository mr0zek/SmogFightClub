using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistration
{
  internal interface IAccountRepository
  {
    void Add(Account account);
    Account Get(string id);
  }
}