using System;
using System.Threading.Tasks;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.SharedKernel;
using static SFC.Sensors.Features.RegisterMeasurement.Contract.RegisterMeasurementCommand;

namespace SFC.Sensors.Features.RegisterMeasurement
{

  internal interface IMeasurementRepository
  {
    Task Add(Guid sensorId, DateTime date, PolutionType polutionType, decimal value);
  }
}