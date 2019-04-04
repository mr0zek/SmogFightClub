using System;
using System.Collections.Generic;
using System.Text;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.CreateAccount.Contract
{
  public class AccountCreatedEvent
  {
    public LoginName LoginName { get; set; }
  }
}
