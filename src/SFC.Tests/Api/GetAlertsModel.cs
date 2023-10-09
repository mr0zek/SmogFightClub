using System.Collections.Generic;

namespace SFC.Tests.Api
{
    public class GetAlertsModel
    {
        public IEnumerable<GetAlertModel> Alerts { get; set; }
    }
}