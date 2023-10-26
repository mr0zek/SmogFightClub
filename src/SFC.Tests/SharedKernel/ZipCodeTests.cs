using Newtonsoft.Json;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SFC.Tests.SharedKernel
{
  public class ZipCodeTests
  {
    [Fact]
    public void ZipCodeJsonSerialization()
    {
      ZipCode z = "12-134";

      var data = JsonConvert.SerializeObject(z, Formatting.Indented, new ZipCodeJsonConverter() );
      var z2 = JsonConvert.DeserializeObject<ZipCode>(data, new ZipCodeJsonConverter());

      Assert.Equal(z, z2);
    }
  }
}
