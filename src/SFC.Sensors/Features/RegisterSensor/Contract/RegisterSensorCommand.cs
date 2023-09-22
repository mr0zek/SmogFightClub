using System;
using SFC.SharedKernel;

namespace SFC.Sensors.Features.RegisterSensor.Contract
{
    public class RegisterSensorCommand
    {
        public LoginName LoginName { get; set; }
        public ZipCode ZipCode { get; set; }
        public Guid Id { get; set; }
    }
}
