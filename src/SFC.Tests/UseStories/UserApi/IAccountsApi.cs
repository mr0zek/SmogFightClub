using System.Threading.Tasks;
using RestEase;

namespace SFC.Tests.UseStories.UserApi
{
  public interface IAccountsApi
  {
    [Post("api/v1.0/accounts")]
    Task<string> PostAccount([Body]PostAccountModel account);

    [Post("api/v1.0/accounts/{id}/confirmations")]
    Task PostAccountConfirmation([Path]string id);
  }
}