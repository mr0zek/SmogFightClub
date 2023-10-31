using SFC.SharedKernel;
using System;
using System.Threading.Tasks;

namespace SFC.Processes.Features.UserRegistration
{
  internal interface IAccountRepository
  {
    Task Add(Account account);
    Task<Account> Get(Guid id);
  }
}