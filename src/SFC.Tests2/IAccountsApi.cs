using System.Threading.Tasks;
using RestEase;

namespace SFC.Tests
{
  public interface IAccountsApi
  {
    [Post("api/v1.0/accounts")]
    Task<PostAccountResponse> PostAccount([Body]PostAccountModel account);

    [Put("api/v1.0/accounts/confirmation/{id}")]
    Task PostAccountConfirmation([Path]string id);
  }
}