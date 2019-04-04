using System;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  public interface ISensorRepository
  {
    bool Exits(Guid commandSensorId);
  }
}