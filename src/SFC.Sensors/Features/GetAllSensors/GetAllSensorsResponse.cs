using SFC.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace SFC.Sensors.Features.GetAllSensors
{
  public class GetAllSensorsResponse : IResponse
  {
    public GetAllSensorsResponse(IEnumerable<SensorReadModel> sensors)
    {
      Sensors = sensors;
    }
    public class SensorReadModel : IResponse
    {
      public string ZipCode { get; set; }
    }

    public IEnumerable<SensorReadModel> Sensors { get; }
  }
}