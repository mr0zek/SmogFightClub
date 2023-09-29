using System;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition.Contract
{
  public class RegisterAlertConditionCommand : ICommand
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid Id { get; set; }
  }
}