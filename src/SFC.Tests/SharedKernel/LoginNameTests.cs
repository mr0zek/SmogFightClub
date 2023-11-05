using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.SharedKernel
{
  public class LoginNameTests
  {
    [Fact]
    public void LoginNameJsonSerialization()
    {
      LoginName z = "ala ma kota";

      var data = JsonSerializer.Serialize(z);
      var z2 = JsonSerializer.Deserialize(data, typeof(LoginName));

      Assert.Equal(z, z2);
    }


  }
}
