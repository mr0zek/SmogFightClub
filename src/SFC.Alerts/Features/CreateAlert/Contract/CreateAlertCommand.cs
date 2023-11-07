using System;
using SFC.Infrastructure.Interfaces.Communication;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.CreateAlert.Contract
{
  public class CreateAlertCommand : ICommand
  {
    public CreateAlertCommand(LoginName loginName, ZipCode zipCode, Guid id)
    {
      LoginName = loginName;
      ZipCode = zipCode;
      Id = id;
    }

    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
    public Guid Id { get; set; }
  }
}