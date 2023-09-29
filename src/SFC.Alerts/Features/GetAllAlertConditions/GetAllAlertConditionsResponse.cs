using System.Collections.Generic;
using SFC.Alerts.Features.GetAllAlertCondition;
using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.GetAllAlertConditions
{
  public class GetAllAlertConditionsResponse : IResponse
    {
        public IEnumerable<AlertConditionResponse> Alerts { get; }

        public GetAllAlertConditionsResponse(IEnumerable<AlertConditionResponse> alerts)
        {
            Alerts = alerts;
        }
    }
}