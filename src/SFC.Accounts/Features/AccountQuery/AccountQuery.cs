using SFC.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace SFC.Accounts.Features.AccountQuery
{
  public class AccountQuery : IRequest<AccountsReadModel>
  {
    public int Skip { get; set; }
    public int Take { get; set; }
  }
}