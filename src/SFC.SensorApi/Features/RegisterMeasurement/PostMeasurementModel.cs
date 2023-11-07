using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.SensorApi.Features.RegisterMeasurement
{
  public class PostMeasurementModel : ICommand
  {
    public PostMeasurementModel(Dictionary<PolutionType, decimal> values)
    {
      Values = values;
    }

    public Dictionary<PolutionType, decimal> Values { get; set; }
  }

  
}