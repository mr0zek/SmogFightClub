using SFC.Infrastructure.Interfaces.Communication;
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
      public SensorReadModel(string zipCode)
      {
        ZipCode = zipCode;
      }

      public string ZipCode { get; set; }
    }

    public IEnumerable<SensorReadModel> Sensors { get; }
  }
}