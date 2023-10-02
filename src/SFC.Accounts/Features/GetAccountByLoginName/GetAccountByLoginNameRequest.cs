using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.GetAccountByLoginName
{
  public class GetAccountByLoginNameRequest : IRequest<GetAccountByLoginNameResponse>
  {
    public GetAccountByLoginNameRequest(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; internal set; }
  }
}
