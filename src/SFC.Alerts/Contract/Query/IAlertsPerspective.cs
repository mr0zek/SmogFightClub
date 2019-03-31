namespace SFC.Alerts.Contract.Query
{
  public interface IAlertsPerspective
  {
    AlertsReadModel GetAll(string loginName);
    AlertReadModel Get(string id, string loginName);
  }
}
