using System.Collections.Generic;

namespace SFC.Tests.Tools.Api
{
    public class GetSensorsResult
    {
        public IEnumerable<GetSensorResult> Sensors { get; set; }
    }
}