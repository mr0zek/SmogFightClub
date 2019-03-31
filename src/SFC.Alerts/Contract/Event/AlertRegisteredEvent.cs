namespace SFC.Alerts.Contract.Event
{
  public class AlertRegisteredEvent
  {
    public string LoginName { get; set; }
    public string ZipCode { get; set; }
  }
}