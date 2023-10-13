using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.VerifySmogExceedence.Contract
{
    public class SmogAlertEvent : IEvent
  {

    public LoginName LoginName { get; set; }

    public SmogAlertEvent(LoginName loginName, ZipCode zipCode)
    {
      LoginName = loginName;
      ZipCode = zipCode;
    }

    public ZipCode ZipCode { get; set; }
  }
}