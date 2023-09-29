using System;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using static SFC.Sensors.Features.RegisterMeasurement.Contract.RegisterMeasurementCommand;

namespace SFC.Sensors.Features.RegisterMeasurement
{

    internal interface IMeasurementRepository
  {
    void Add(Guid sensorId, DateTime date, ElementName elementName, decimal elementValue);
  }  
}