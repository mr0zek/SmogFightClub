using System;
using System.Formats.Asn1;
using System.Net;
using Newtonsoft.Json;

namespace SFC.SharedKernel
{
  public class ZipCodeJsonConverter : JsonConverter<ZipCode>
  {
    public override ZipCode? ReadJson(JsonReader reader, Type objectType, ZipCode? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      return reader.Value?.ToString();
    }

    public override void WriteJson(JsonWriter writer, ZipCode? value, JsonSerializer serializer)
    {
      writer.WriteValue(value.ToString());
    }
  }
}