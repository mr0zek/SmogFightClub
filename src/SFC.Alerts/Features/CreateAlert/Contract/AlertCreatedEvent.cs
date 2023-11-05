using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.CreateAlert.Contract
{
  public class AlertCreatedEvent : IEvent
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }

    public AlertCreatedEvent(LoginName loginName, ZipCode zipCode)
    {
      LoginName = loginName;
      ZipCode = zipCode;  
    }
  }
}