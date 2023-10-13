using System;
using System.Collections.Generic;
using System.Text;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.CreateAccount.Contract
{
    public class AccountCreatedEvent : IEvent
  {
    public AccountCreatedEvent(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; set; }    
  }
}
