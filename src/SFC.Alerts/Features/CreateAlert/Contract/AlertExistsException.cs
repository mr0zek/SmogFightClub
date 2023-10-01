using System;
using SFC.SharedKernel;

namespace SFC.Alerts.Features.CreateAlert.Contract
{
  internal class AlertExistsException : Exception
  {
    public ZipCode ZipCode { get; }

    public AlertExistsException(ZipCode zipCode)
    {
      ZipCode = zipCode;
    }
  }
}