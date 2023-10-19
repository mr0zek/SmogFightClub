using SFC.Infrastructure.Interfaces.Communication;

namespace SFC.UserApi.Features.Sensors
{
  public class PostSensorModel : ICommand
  {
    public string ZipCode { get; set; }
  }
}