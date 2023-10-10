using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Sensors.Features.GetSensor
{
    public class GetSensorResponse : IResponse
    {
      public Guid Id { get; set; } 
      public string ZipCode { get; set; }
    }
}