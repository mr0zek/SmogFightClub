using SFC.Infrastructure.Interfaces.Communication;
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