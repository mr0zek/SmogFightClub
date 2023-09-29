using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition.Contract
{
  public class AlertCreatedEvent : IEvent
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}