using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.Tests.Tools.Api
{
    public class PostMeasurementModel
    {
        public Dictionary<PolutionType, decimal> Values { get; set; } = new Dictionary<PolutionType, decimal>();
    }
}