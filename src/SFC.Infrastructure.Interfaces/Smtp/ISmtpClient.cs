using SFC.Infrastructure.Interfaces.Documentation;
using SFC.SharedKernel;

namespace SFC.Infrastructure.Interfaces.Smtp
{  
  public interface ISmtpClient
  {
    [ExitPointTo("SmtpProvider", CallType.Command)]
    Task Send(Email email, string title, string body);
  }
}