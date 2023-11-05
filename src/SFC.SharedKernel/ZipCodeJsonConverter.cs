using System;
using System.Formats.Asn1;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFC.SharedKernel
{
  public class ZipCodeJsonConverter : JsonConverter<ZipCode>
  {
    public override ZipCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, ZipCode value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString());
    }
  }
}