using SFC.Infrastructure.Interfaces.Communication;
using System;
using System.Collections.Generic;

namespace SFC.Sensors.Features.GetAllSensors
{
    public class GetAllSensorsResponse : IResponse
  {
    public GetAllSensorsResponse(IEnumerable<SensorReadModel> sensors)
    {
      Sensors = sensors;
    }
    public class SensorReadModel : IResponse
    {
      public SensorReadModel(Guid id, string zipCode)
      {
        Id = id;
        ZipCode = zipCode;
      }

      public Guid Id { get; }
      public string ZipCode { get; set; }
    }

    public IEnumerable<SensorReadModel> Sensors { get; }
  }
}