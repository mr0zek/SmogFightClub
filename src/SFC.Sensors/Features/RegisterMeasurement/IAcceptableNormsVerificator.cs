using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal interface IAcceptableNormsVerificator
  {
    bool Verify(Dictionary<PolutionType, decimal> elements);
  }
}