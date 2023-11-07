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
  public class EmailTests
  {
    [Fact]
    public void EmailJsonSerialization()
    {
      Email z = "ala@makotowska.pl";

      var data = JsonSerializer.Serialize(z);
      var z2 = JsonSerializer.Deserialize(data, typeof(Email));

      Assert.Equal(z, z2);
    }

    [Fact]
    public void NullIsAllowed() 
    {      
      Email e = (string)null; // implicit conversion kicks in
      Assert.Equal(null, e);
    }
  }
}
