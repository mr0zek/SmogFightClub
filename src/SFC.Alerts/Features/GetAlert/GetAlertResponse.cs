using SFC.Infrastructure.Interfaces;
using System;

namespace SFC.Alerts.Features.GetAlert
{
  public class GetAlertResponse : IResponse
  {
    public Guid Id { get; set; }
    public string ZipCode { get; set; }
  }
}