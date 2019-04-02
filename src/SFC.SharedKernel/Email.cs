using System.Collections.Generic;

namespace SFC.SharedKernel
{
  public class Email : ValueObject
  {
    private readonly string _value;

    private Email(string value)
    {
      _value = value;
    }

    public override string ToString()
    {
      return _value;
    }

    public static implicit operator Email(string email)
    {
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