using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
  public static class ObjectExtensions
  {
    public static T ThrowIfNull<T>(this T? obj)
    {
      if (obj == null)
      {
        throw new ArgumentNullException(nameof(obj));
      }
      return obj;
    }
  }
}
