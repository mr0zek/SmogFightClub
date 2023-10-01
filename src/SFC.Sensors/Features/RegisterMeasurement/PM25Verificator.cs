using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  class PM25Verificator : IAcceptableNormsVerificator
  {
    public bool Verify(Dictionary<PolutionType, decimal> elements)
    {
      if (elements.ContainsKey(PolutionType.PM2_5))
      {
        if (elements[PolutionType.PM2_5] > 15)
        {
          return false;
        }
      }
      return true;
    }
  }
}