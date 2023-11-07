using System;
using Automatonymous;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SFC.Processes.Features.UserRegistrationSaga
{
  public class SagaStateJSonConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      var state = (State)value.ThrowIfNull();

      writer.WriteValue(state.Name);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {      
      JToken jt = JToken.Load(reader);
      string value = (jt.Value<string>()).ThrowIfNull();

      return UserRegistrationSagaData.States[value].ThrowIfNull();
    }


    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(State);
    }
  }
}