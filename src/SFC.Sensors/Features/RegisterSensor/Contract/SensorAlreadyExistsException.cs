using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Sensors.Features.RegisterSensor.Contract
{
  public class SensorAlreadyExistsException : Exception
  {
    public Guid SensorId { get; set; }

    public SensorAlreadyExistsException(Guid sensorId)
    {
      SensorId = sensorId;
    }
  }
}
