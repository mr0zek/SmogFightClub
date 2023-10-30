using SFC.SharedKernel;
using System;
using System.Threading.Tasks;

namespace SFC.Sensors.Features.RegisterSensor
{
  internal interface ISensorRepository
  {
    Task Add(Guid sensorId, ZipCode zipCode, LoginName loginName);
    Task<bool> Exits(Guid sensorId);
  }
}