using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace SFC.Tests.UserApi
{
  public interface IUserApi
  {
    [Header("Authorization")]
    string Token { get; set; }

    [Post("api/v1.0/accounts")]
    Task<string> PostAccount([Body] PostAccountModel account);

    [Post("api/v1.0/accounts/{id}/confirmation")]
    Task PostAccountConfirmation([Path] string id);

    [Post("api/v1.0/user/sensors")]
    Task<Guid> PostSensor([Body] PostSensorModel model);

    [Post("api/v1.0/user/alerts")]
    Task<Guid> PostAlert([Body] PostAlertModel model);

    [Post("api/v1.0/user/")]
    Task PostUser([Body] PostUserModel model);

    [Get("api/v1.0/user/alerts")]
    Task<GetAlertsModel> GetAlerts();
  }
}