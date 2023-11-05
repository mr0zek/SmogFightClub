using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SFC.SharedKernel
{
  [JsonConverter(typeof(ZipCodeJsonConverter))]
  public class ZipCode : ValueObject
  {
    private readonly string _value;

    protected ZipCode() { } 

    public ZipCode(string value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
      return _value;
    }

    public static implicit operator ZipCode(string zipCode)
    {
      if (zipCode == null)
      {
        return null;
      }
      return new ZipCode(zipCode);
    }

    public static implicit operator string(ZipCode zipCode)
    {
      return zipCode.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return _value;
    }
  }
}