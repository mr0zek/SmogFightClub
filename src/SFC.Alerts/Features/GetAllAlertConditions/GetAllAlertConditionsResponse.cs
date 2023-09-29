using System.Collections.Generic;
using SFC.Alerts.Features.GetAllAlertCondition;

namespace SFC.Alerts.Features.GetAllAlertConditions
{
  public class GetAllAlertConditionsResponse
    {
        public IEnumerable<AlertConditionResponse> Alerts { get; }

        public GetAllAlertConditionsResponse(IEnumerable<AlertConditionResponse> alerts)
        {
            Alerts = alerts;
        }
    }
}