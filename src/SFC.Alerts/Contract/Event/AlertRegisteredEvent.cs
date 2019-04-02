using SFC.SharedKernel;

namespace SFC.Alerts.Contract.Event
{
  public class AlertRegisteredEvent
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}