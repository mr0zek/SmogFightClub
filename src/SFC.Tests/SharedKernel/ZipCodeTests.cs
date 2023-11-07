using SFC.SharedKernel;
using System.Text.Json;
using Xunit;

namespace SFC.Tests.SharedKernel
{
  public class ZipCodeTests
  {
    [Fact]
    public void ZipCodeJsonSerialization()
    {
      ZipCode z = "12-134";

      var data = JsonSerializer.Serialize(z);
      var z2 = JsonSerializer.Deserialize(data, typeof(ZipCode));

      Assert.Equal(z, z2);
    }

    [Fact]
    public void NullIsAllowed()
    {
      ZipCode e = (string)null; // implicit conversion kicks in
      Assert.Equal(null, e);
    }
  }
}
