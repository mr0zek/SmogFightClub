using System.Collections.Generic;

namespace SFC.SensorApi.Features.RecordMeasurement
{
  public class PostMeasurementModel
  {
    public Dictionary<string, decimal> Values{get; set; }
  }
}