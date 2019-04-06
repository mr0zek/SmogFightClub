using System;
using System.Collections.Generic;

namespace SFC.SharedKernel
{
  public class LoginName : ValueObject
  {
    private readonly string _value;

    private LoginName(string value)
    {
      _value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
      return _value;
    }

    public static implicit operator LoginName(string loginName)
    {
      if (loginName == null)
      {
        return null;
      }
      return new LoginName(loginName);
    }

    public static implicit operator string(LoginName loginName)
    {
      return loginName.ToString();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return _value;
    }
  }
}