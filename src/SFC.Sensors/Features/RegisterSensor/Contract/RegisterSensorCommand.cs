using System;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Sensors.Features.RegisterSensor.Contract
{
  public class RegisterSensorCommand : ICommand
  {
    public RegisterSensorCommand(LoginName loginName, ZipCode zipCode, Guid sensorId)
    {
      LoginName = loginName;
      ZipCode = zipCode;
      SensorId = sensorId;
    }

    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid SensorId { get; set; }
  }
}
