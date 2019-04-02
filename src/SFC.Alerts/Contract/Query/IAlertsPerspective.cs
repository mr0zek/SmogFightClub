using SFC.SharedKernel;

namespace SFC.Alerts.Contract.Query
{
  public interface IAlertsPerspective
  {
    AlertsReadModel GetAll(LoginName loginName);
    AlertReadModel Get(string id, LoginName loginName);
  }
}
