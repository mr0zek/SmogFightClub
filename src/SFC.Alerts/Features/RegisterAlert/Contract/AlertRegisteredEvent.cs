using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlert.Contract
{
  public class AlertRegisteredEvent
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}