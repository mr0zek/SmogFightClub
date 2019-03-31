using System.Collections.Generic;

namespace SFC.Alerts.Contract.Query
{
  public class AlertsReadModel
  {
    public IEnumerable<AlertReadModel> Alerts { get; }

    public AlertsReadModel(IEnumerable<AlertReadModel> alerts)
    {
      Alerts = alerts;
    }
  }
}