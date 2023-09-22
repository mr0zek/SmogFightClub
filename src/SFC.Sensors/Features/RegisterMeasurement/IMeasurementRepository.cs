using System;
using SFC.Sensors.Features.RegisterMeasurement.Contract;

namespace SFC.Sensors.Features.RegisterMeasurement
{

  internal interface IMeasurementRepository
  {
    void Add(Guid sensorId, DateTime date, ElementName elementName, decimal elementValue);
  }  
}