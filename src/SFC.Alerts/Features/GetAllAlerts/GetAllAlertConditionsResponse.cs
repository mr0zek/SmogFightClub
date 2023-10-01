using System.Collections.Generic;
using SFC.Alerts.Features.GetAllAlert;
using SFC.Infrastructure.Interfaces;

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