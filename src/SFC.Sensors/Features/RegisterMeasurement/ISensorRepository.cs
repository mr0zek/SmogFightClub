using System;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal interface ISensorRepository
  {
    bool Exits(Guid commandSensorId);
  }
}