using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.GetAllAlertCondition
{
  public class GetAlertConditionResponse : IResponse
  {
    public int Id { get; set; }
    public string ZipCode { get; set; }
  }
}