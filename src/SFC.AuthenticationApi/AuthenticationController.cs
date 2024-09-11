using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFC.AuthenticationApi.Features.Authentication;

namespace SFC.AuthenticationApi
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]  
  public class AuthenticationController : Controller
  {
    private readonly ITokenRepository _tokenRepository;

    public AuthenticationController(ITokenRepository tokenRepository)
    {
      _tokenRepository = tokenRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(CredentialsModel usersdata)
    {
      var token = await _tokenRepository.Authenticate(usersdata);

      if (token == null)
      {
        return Unauthorized();
      }

      return Ok(token);
    }
  }
}