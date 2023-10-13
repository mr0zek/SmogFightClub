using System.Collections.Generic;
using SFC.Alerts.Features.GetAlert;
using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.Alerts.Features.GetAllAlerts
{
    public class GetAllAlertsResponse : IResponse
    {
        public IEnumerable<AlertResponse> Alerts { get; }

        public GetAllAlertsResponse(IEnumerable<AlertResponse> alerts)
        {
            Alerts = alerts;
        }
    }
}