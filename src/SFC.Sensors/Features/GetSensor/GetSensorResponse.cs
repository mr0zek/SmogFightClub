using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Sensors.Features.GetSensor
{
  public class GetSensorResponse : IResponse
  {
    public GetSensorResponse(Guid id, string zipCode)
    {
      Id = id;
      ZipCode = zipCode;
    }

    public Guid Id { get; set; }
    public string ZipCode { get; set; }
  }
}