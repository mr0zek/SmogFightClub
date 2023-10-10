using System.Collections.Generic;

namespace SFC.Tests.Api
{
  public class GetSensorsModel
  {
    public IEnumerable<GetSensorModel> Sensors { get; set; }
  }
}