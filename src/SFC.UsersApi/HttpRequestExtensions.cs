using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SFC.UserApi
{
  static class HttpRequestExtensions
  {
    public static string BaseUrl(this HttpRequest req)
    {
      return BaseUrl(req, null);
    }
    public static string BaseUrl(this HttpRequest req, string? path)
    {      
      var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
      if (uriBuilder.Uri.IsDefaultPort)
      {
        uriBuilder.Port = -1;
      }

      if (string.IsNullOrEmpty(path))
      {
        return uriBuilder.Uri.AbsoluteUri;
      }

      if (path[0] == '/')
      {
        return uriBuilder.Uri.AbsoluteUri + path.Skip(1);
      }

      return uriBuilder.Uri.AbsoluteUri + path;
    }
  }
}