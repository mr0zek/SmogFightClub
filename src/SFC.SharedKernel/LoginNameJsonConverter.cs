using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFC.SharedKernel
{
  class LoginNameJsonConverter : JsonConverter<LoginName>
  {
    public override LoginName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetString() ?? throw new NullReferenceException();
    }
    
    public override void Write(Utf8JsonWriter writer, LoginName value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}