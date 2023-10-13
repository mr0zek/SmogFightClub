using System.Collections.Generic;

namespace SFC.Tests.Api
{
  public class GetSensorsResult
  {
    public IEnumerable<GetSensorResult> Sensors { get; set; }
  }
}