using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Features.GetSensor
{
  public class GetSensorRequest : IRequest<GetSensorResponse>
  {
    public GetSensorRequest(Guid sensorId, LoginName loginName)
    {
      SensorId = sensorId;
      LoginName = loginName;  
    }

    public Guid SensorId { get; internal set; }
    public LoginName LoginName { get; internal set; }
  }
}