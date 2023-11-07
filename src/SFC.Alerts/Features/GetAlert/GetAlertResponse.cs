﻿using SFC.Infrastructure.Interfaces.Communication;
using System;

namespace SFC.Alerts.Features.GetAlert
{
  public class GetAlertResponse : IResponse
  {
    public GetAlertResponse(Guid id, string zipCode)
    {
      Id = id;
      ZipCode = zipCode;
    }

    public Guid Id { get; set; }
    public string ZipCode { get; set; }
  }
}