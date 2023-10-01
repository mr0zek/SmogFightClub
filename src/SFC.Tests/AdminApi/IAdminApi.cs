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
    [Get("api/v1.0/alertNotificationsWithUserData")]
    Task<AlertNotificationsWithUserDataResult> GetAlertNotificationsWithUserData([Query] int skip, int take);
  }
}
