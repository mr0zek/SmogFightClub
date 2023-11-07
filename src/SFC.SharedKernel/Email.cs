using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SFC.SharedKernel
{

  [JsonConverter(typeof(EmailJsonConverter))]
  public class Email : ValueObject
  {
    private readonly string _value;

    private Email(string value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
      return _value;
    }

    public static implicit operator Email(string email)
    {
      if (email == null)
      {
        throw new ArgumentNullException(nameof(email));
      }
      return new Email(email);
    }

    public static implicit operator string(Email email)
    {
      return email.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return _value;
    }
  }
}