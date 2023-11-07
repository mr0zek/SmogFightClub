using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Tests.Tools.Api
{
    public class SearchableDashboardResult
    {
        public class SearchableDashboardEntryModel
        {
            public string? LoginName { get; set; }
            public int AlertsCount { get; set; }
        }

        public IEnumerable<SearchableDashboardEntryModel>? Results { get; set; }
    }
}