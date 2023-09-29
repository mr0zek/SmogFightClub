using System.Collections.Generic;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces;

namespace SFC.Accounts.Features.SearchAccount
{
    public class SearchAccountResponse : IResponse
    {
        public SearchAccountResponse(IEnumerable<GetAccountByLoginNameResponse> accounts)
        {
            Accounts = accounts;
        }

        public IEnumerable<GetAccountByLoginNameResponse> Accounts { get; set; }
    }
}