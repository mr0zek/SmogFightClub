using System.Data;
using System.Data.SqlClient;
using SFC.SharedKernel;

namespace SFC.Alerts
{
  internal interface IAlertsRepository
  {
    void Add(ZipCode zipCode, LoginName loginName);
    bool Exists(ZipCode zipCode, LoginName loginName);
  }
}