using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.UserApi.Features.Alerts
{
  public class PostAlertModel : ICommand
  {
    public string? ZipCode { get; set; }
  }
}