using System;
using System.Collections.Generic;
using System.Text;
using SFC.SharedKernel;

namespace SFC.Sensors.Contract.Command
{
  public class RegisterSensorCommand
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid Id { get; set; }
  }
}
