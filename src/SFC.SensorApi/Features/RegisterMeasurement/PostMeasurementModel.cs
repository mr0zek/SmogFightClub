using System;
using System.Collections.Generic;

namespace SFC.SensorApi.Features.RegisterMeasurement
{
  public class PostMeasurementModel
  {
    public Dictionary<string, decimal> Values { get; set; }
  }
}