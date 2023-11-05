using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFC.SharedKernel
{
  public class LoginNameJsonConverter : JsonConverter<LoginName>
  {
    public override LoginName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetString();
    }
    
    public override void Write(Utf8JsonWriter writer, LoginName value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}