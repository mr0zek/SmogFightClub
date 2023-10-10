using SFC.SharedKernel;
using System;

namespace SFC.Processes.Features.UserRegistration
{
  internal interface IAccountRepository
  {
    void Add(Account account);
    Account Get(Guid id);
  }
}