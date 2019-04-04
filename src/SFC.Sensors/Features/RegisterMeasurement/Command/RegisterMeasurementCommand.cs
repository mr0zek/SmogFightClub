using System;
using System.Collections.Generic;

namespace SFC.Sensors.Features.RegisterMeasurement.Command
{
  public class RegisterMeasurementCommand
  {
    public Guid SensorId { get; set; }
    public Dictionary<ElementName, decimal> Elements { get; set; } = new Dictionary<ElementName, decimal>();
    public DateTime Date { get; set; }
    public Guid Id { get; set; }
  }
}