using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using SFC.Infrastructure.Interfaces.Smtp;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Features.SmtpIntegration
{
  class FakeSmtpClient : ISmtpClient
  {
    public async Task Send(Email email, string title, string body)
    {
      Log.Debug("mail sent to: {email}, with title: {title} and body: {body}", email, title, body);
    }
  }
}