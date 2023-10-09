using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SFC.Accounts.Features.Authenticate;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SFC.AuthenticationApi.Features.Authentication
{
  public class TokenRepository : ITokenRepository
  {    
    private readonly IQuery _accountQuery;
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration, IQuery accountQuery)
    {
      _configuration = configuration;
      _accountQuery = accountQuery;
    }


    public string? Authenticate(CredentialsModel credentials)
    {
      if (!_accountQuery.Query(new AuthenticateRequest(credentials.LoginName,PasswordHash.FromPassword(credentials.Password))).Success)      
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
             new Claim(ClaimTypes.Name, credentials.LoginName)
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
