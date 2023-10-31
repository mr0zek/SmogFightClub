using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFC.Alerts.Features.CreateAlert
{
  internal interface IAlertWriteRepository
  {
    Task Add(Guid id, ZipCode zipCode, LoginName loginName);
    Task<bool> Exists(ZipCode zipCode, LoginName loginName);
  }
}