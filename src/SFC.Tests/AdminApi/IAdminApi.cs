using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Tests.AdminApi
{
  public interface IAdminApi
  {
    [Get("api/v1.0/admin/alertNotificationsWithUserData")]
    Task<AlertNotificationsWithUserDataResult> GetAlertNotificationsWithUserData([Query] int skip, int take);
    [Get("api/v1.0/admin/searchableDashboard")]
    Task<SearchableDashboardResult> GetSearchableDashboard([Query] int skip, [Query] int take, [Query] int alertsMin, [Query] int alertsMax);
  }
}
