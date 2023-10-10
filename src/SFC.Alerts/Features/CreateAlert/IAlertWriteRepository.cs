using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.Alerts.Features.CreateAlert
{
  internal interface IAlertWriteRepository
  {
    void Add(Guid id, ZipCode zipCode, LoginName loginName);
    bool Exists(ZipCode zipCode, LoginName loginName);
  }
}