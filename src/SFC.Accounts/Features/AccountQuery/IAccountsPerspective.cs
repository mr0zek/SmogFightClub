namespace SFC.Accounts.Features.AccountQuery
{
  public interface IAccountsPerspective
  {
    AccountReadModel Get(string loginName);
    AccountsReadModel Search(AccountQuery accountQuery);
  }
}
