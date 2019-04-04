using SFC.SharedKernel;

namespace SFC.Alerts.Features.AlertQuery
{
  public interface IAlertsPerspective
  {
    AlertsReadModel GetAll(LoginName loginName);
    AlertReadModel Get(string id, LoginName loginName);
  }
}
