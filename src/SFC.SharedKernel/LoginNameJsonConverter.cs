using System;
using Newtonsoft.Json;

namespace SFC.SharedKernel
{
  public class LoginNameJsonConverter : JsonConverter<LoginName>
  {
    public override LoginName? ReadJson(JsonReader reader, Type objectType, LoginName? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      return reader.Value?.ToString();
    }

    public override void WriteJson(JsonWriter writer, LoginName? value, JsonSerializer serializer)
    {
      writer.WriteValue(value.ToString());
    }
  }
}