using SFC.Accounts.Features.CreateCount;

namespace SFC.Accounts.Features.AccountQuery
{
  public interface IAccountsPerspective
  {
    AccountReadModel Get(string loginName);
  }
}
