using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Accounts.Features.GetAccountByLoginName
{
    public class GetAccountByLoginNameResponse : IResponse
    {
        public LoginName LoginName { get; set; }
    }
}