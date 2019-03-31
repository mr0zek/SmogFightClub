using System;

namespace SFC.Alerts.Contract.Command
{
  internal class AlertExistsException : Exception
  {
    public string ZipCode { get; }

    public AlertExistsException(string zipCode)
    {
      ZipCode = zipCode;
    }
  }
}