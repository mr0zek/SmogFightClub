using System.Data;
using System.Data.SqlClient;

namespace SFC.Alerts
{
  internal interface IAlertsRepository
  {
    void Add(string zipCode, string loginName);
    bool Exists(string zipCode, string loginName);
  }
}