using Microsoft.AspNetCore.Http;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Infrastructure
{
  internal class IdentityProvider : IIdentityProvider
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }
    public LoginName GetLoginName()
    {
      return _httpContextAccessor.HttpContext.User.Identity.Name;
    }
  }
}
