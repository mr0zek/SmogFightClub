using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace SFC.Tests.Api
{
    public interface IApi
  {
    [Header("Authorization")]
    string Token { get; set; }

    [Post("api/v1.0/accounts")]
    Task<Guid> PostAccount([Body] PostAccountModel account);

    [Post("api/v1.0/accounts/{id}/confirmation")]
    Task PostAccountConfirmation([Path] Guid id);

    [Post("api/v2.0/accounts")]
    Task<string> PostAccountV2([Body] PostAccountModel account);

    [Post("api/v2.0/accounts/{id}/confirmation")]
    Task PostAccountConfirmationV2([Path] string id);

    [Post("api/v1.0/user/sensors")]
    Task<Guid> PostSensor([Body] PostSensorModel model);

    [Post("api/v1.0/user/alerts")]
    Task<Guid> PostAlert([Body] PostAlertModel model);

    [Post("api/v1.0/user/")]
    Task PostUser([Body] PostUserModel model);

    [Get("api/v1.0/user/alerts")]
    Task<GetAlertsModel> GetAlerts();

    [Post("api/v1.0/sensors/{sensorId}/measurements")]
    Task<string> PostMeasurements([Path] Guid sensorId, [Body] PostMeasurementModel model);    

    [Get("api/v1.0/admin/alertNotificationsWithUserData")]
    Task<AlertNotificationsWithUserDataResult> GetAlertNotificationsWithUserData([Query] int skip, int take);
    [Get("api/v1.0/admin/searchableDashboard")]
    Task<SearchableDashboardResult> GetSearchableDashboard([Query] int skip, [Query] int take, [Query] int alertsMin, [Query] int alertsMax);

    [Post("api/v1.0/authentication")]
    Task<string> Login([Body] CredentialsModel model);

    [Get("api/v1.0/user/alerts/{id}")]
    Task<GetAlertModel> GetAlert([Path]Guid id);
  }
}