using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Sensors.Features.GetAllSensors
{
  public class GetAllSensorsRequest : IRequest<GetAllSensorsResponse>
  {
    public GetAllSensorsRequest(LoginName loginName)
    {
      LoginName = loginName;
    }

    public LoginName LoginName { get; set; }
  }
}