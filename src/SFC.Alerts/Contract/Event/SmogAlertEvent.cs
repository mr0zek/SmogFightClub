﻿using SFC.SharedKernel;

namespace SFC.Alerts.Contract.Event
{
  public class SmogAlertEvent
  {
    public LoginName LoginName { get; set; }
    public ZipCode ZipCode { get; set; }
  }
}