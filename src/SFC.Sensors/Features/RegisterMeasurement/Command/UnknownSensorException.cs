using System;

namespace SFC.Sensors.Features.RegisterMeasurement.Command
{
  public class UnknownSensorException : Exception
  {
    public Guid SensorId { get; }

    public UnknownSensorException(Guid sensorId) : base($"Unknown sensor : {sensorId}")
    {
      SensorId = sensorId;
    }
  }
}