using System;
using System.Collections.Generic;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.SearchAccount
{
  public class SearchAccountResponse : IResponse
  {
    public SearchAccountResponse(IEnumerable<Account> accounts)
    {
      Accounts = accounts;
    }

    public IEnumerable<Account> Accounts { get; set; }

    public class Account : IResponse
    {
      public LoginName LoginName { get; set; }
      public int Id {  get; set; }

      public Account(Int64 id, string loginName) : this((int)id, (LoginName)loginName) 
      {
      }

      public Account(int id, LoginName loginName)
      {
        LoginName = loginName;
        Id = id;
      }
    }
  }
}