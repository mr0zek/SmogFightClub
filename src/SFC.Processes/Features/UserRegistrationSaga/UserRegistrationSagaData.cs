using System;
using System.Collections;
using System.Collections.Generic;
using Automatonymous;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    [JsonConverter(typeof(StateJSonConverter))]
    public State CurrentState { get; set; }

    [JsonIgnore]
    public static IDictionary<string, State> States { get; set; }

  }

  public class StateJSonConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(((State)value).Name);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
        return null;

      JToken jt = JToken.Load(reader);
      string value = jt.Value<string>();

      return UserRegistrationSagaData.States[value];
    }


    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(State);
    }
  }
}