using System.Collections;
using System.Collections.Generic;
using Automatonymous;
using Newtonsoft.Json;
using SFC.SharedKernel;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  public class UserRegistrationSagaData
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string LoginName { get; set; }
    public string PasswordHash { get; set; }
    public string ZipCode { get; set; }
    public string BaseUrl { get; set; }

    [JsonConverter(typeof(SagaStateJSonConverter))]
    public State CurrentState { get; set; }

    [JsonIgnore]
    public static IDictionary<string, State> States { get; set; }

  }
}