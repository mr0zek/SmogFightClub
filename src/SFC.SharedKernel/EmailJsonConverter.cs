using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SFC.SharedKernel
{
  class EmailJsonConverter : JsonConverter<Email>
  {
    public override Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}