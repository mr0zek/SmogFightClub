﻿using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Infrastructure.Interfaces.Communication;
using System.Collections.Generic;

namespace SFC.Accounts.Features.SearchAccount
{
    public class SearchAccountRequest : IRequest<SearchAccountResponse>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}