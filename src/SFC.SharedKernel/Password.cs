using System.Security.Cryptography;
using System.Text;

namespace SFC.SharedKernel
{
  public class PasswordHash
  {
    string _hash;

    public string Value { get => _hash; }

    public PasswordHash(string hash)
    {
      _hash = hash;
    }

    public static PasswordHash FromPassword(string password)
    {
      var md5 = new MD5CryptoServiceProvider();
      return new PasswordHash(Encoding.ASCII.GetString(md5.ComputeHash(Encoding.ASCII.GetBytes(password))));
    }
  }
}