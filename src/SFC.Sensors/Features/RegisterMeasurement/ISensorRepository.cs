using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal interface ISensorRepository
  {
    void Add(Guid sensorId, ZipCode zipCode, LoginName loginName);
    bool Exits(Guid sensorId);
    Sensor Get(Guid sensorId);
  }
}