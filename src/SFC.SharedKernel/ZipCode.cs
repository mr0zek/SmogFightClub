using System.Collections.Generic;

namespace SFC.SharedKernel
{
  public class ZipCode : ValueObject
  {
    private readonly string _value;

    private ZipCode(string value)
    {
      _value = value;
    }

    public override string ToString()
    {
      return _value;
    }

    public static implicit operator ZipCode(string zipCode)
    {
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