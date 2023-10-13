using System.Collections.Generic;

namespace SFC.Tests.Api
{
    public class GetAlertsModel
    {
        public IEnumerable<GetAlertResult> Alerts { get; set; }
    }
}