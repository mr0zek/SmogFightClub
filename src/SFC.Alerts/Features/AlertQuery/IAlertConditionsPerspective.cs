using SFC.SharedKernel;

namespace SFC.Alerts.Features.AlertQuery
{
  public interface IAlertConditionsPerspective
  {
    AlertsReadModel GetAll(LoginName loginName);
    AlertReadModel Get(string id, LoginName loginName);
  }
}
