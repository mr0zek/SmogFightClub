using SFC.Infrastructure.Interfaces;

namespace SFC.Alerts.Features.GetAllAlert
{
  public class GetAlertResponse : IResponse
  {
    public int Id { get; set; }
    public string ZipCode { get; set; }
  }
}