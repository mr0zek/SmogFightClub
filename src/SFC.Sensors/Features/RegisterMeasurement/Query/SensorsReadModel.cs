using System.Collections.Generic;

namespace SFC.Sensors.Features.RegisterMeasurement.Query
{
  public class SensorsReadModel
  {
    public SensorsReadModel(IEnumerable<SensorReadModel> sensors)
    {
      Sensors = sensors;
    }

    public IEnumerable<SensorReadModel> Sensors { get; }
  }
}