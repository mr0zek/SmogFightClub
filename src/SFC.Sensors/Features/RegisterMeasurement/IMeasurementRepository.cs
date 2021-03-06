﻿using System;
using SFC.Sensors.Features.RegisterMeasurement.Command;

namespace SFC.Sensors.Features.RegisterMeasurement
{

  public interface IMeasurementRepository
  {
    void Add(Guid sensorId, DateTime date, ElementName elementName, decimal elementValue);
  }  
}