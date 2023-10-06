using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SFC.AuthenticationApi.Authentication
{
  public class TokenRepository : ITokenRepository
  {
    Dictionary<string, string> UsersRecords = new()
    {
        { "admin","admin"},
        { "password","password"}
    };

    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }


    public string Authenticate(CredentialsModel users)
    {
      if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
      {
        return null;
      }

      //Generate JSON Web Token
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
      var tokenDescriptor = new SecurityTokenDescriptor
      {        
        Subject = new ClaimsIdentity(new Claim[]
        {
             new Claim(ClaimTypes.Name, users.Name)
        }),
        Expires = DateTime.UtcNow.AddMinutes(10),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(tokenKey), 
          SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
