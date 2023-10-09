using System.Security.Cryptography;
using System.Text;

namespace SFC.SharedKernel
{
  public class PasswordHash
  {
    private readonly string _hash;

    public string Value { get => _hash; }

    public PasswordHash(string hash)
    {
      _hash = hash;
    }

    public static PasswordHash FromPassword(string password)
    {
      using (var md5 = MD5.Create())
      {
        var result = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
        return new PasswordHash(Encoding.ASCII.GetString(result));
      }      
    }
  }
}