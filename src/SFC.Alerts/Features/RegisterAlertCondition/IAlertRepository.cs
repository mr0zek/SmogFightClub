using SFC.SharedKernel;

namespace SFC.Alerts.Features.RegisterAlertCondition
{
  internal interface IAlertRepository
  {
    void Add(ZipCode zipCode, LoginName loginName);
    bool Exists(ZipCode zipCode, LoginName loginName);
  }
}