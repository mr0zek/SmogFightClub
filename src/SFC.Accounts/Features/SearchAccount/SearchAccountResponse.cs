using System.Collections.Generic;
using SFC.Accounts.Features.GetAccountByLoginName;

namespace SFC.Accounts.Features.SearchAccount
{
    public class SearchAccountResponse
    {
        public SearchAccountResponse(IEnumerable<GetAccountByLoginNameResponse> accounts)
        {
            Accounts = accounts;
        }

        public IEnumerable<GetAccountByLoginNameResponse> Accounts { get; set; }
    }
}