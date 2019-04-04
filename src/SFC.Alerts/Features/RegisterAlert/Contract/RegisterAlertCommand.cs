using System;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlert.Contract
{
  public class RegisterAlertCommand
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid Id { get; set; }
  }
}