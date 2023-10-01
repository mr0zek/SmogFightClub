using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Tests.SensorApi
{
  /// <summary>
  /// Used library RestEase - https://github.com/canton7/RestEase
  /// </summary>
  public interface ISensorApi
  {
    [Post("api/v1.0/sensors/{sensorId}/measurements")]
    Task<string> PostMeasurements([Path] Guid sensorId, [Body] PostMeasurementModel model);
  }
}
