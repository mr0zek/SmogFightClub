using System;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition.Contract
{
  public class RegisterAlertConditionCommand
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid Id { get; set; }
  }
}