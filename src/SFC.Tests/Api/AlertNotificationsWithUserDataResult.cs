using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Tests.Api
{
    public class AlertNotificationsWithUserDataResult
    {
        public class DashboardEntryModel
        {
            public LoginName LoginName { get; set; }
            public int AlertsSentCount { get; set; }
        }

        public IEnumerable<DashboardEntryModel> Results { get; set; }
    }
}