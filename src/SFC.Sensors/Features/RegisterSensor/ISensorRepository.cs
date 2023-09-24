using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Features.RegisterSensor
{
  internal interface ISensorRepository
  {
    void Add(Guid sensorId, ZipCode zipCode, LoginName loginName);
    bool Exits(Guid sensorId);
  }
}