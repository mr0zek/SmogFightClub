using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.GetAlert
{
  public class AlertResponse : IResponse
  {
    public int Id { get; set; }
    public string ZipCode { get; set; }
  }
}