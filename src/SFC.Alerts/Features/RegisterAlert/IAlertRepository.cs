using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlert
{
  internal interface IAlertsRepository
  {
    void Add(ZipCode zipCode, LoginName loginName);
    bool Exists(ZipCode zipCode, LoginName loginName);
  }
}