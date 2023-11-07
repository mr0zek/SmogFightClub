using SFC.SharedKernel;
using System;

namespace SFC.Sensors.Features.RegisterMeasurement
{
  internal class Sensor
  {
    public Sensor(Guid id, string zipCode) : this(id, (ZipCode) zipCode)
    {
    }

    public Sensor(Guid id, ZipCode zipCode)
    {
      Id = id;
      ZipCode = zipCode;
    }

    public Guid Id { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}